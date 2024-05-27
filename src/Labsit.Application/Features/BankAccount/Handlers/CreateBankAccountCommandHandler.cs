using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Domain.Contracts.Repositories;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Handlers
{
    public class CreateBankAccountCommandHandler(IValidator<CreateBankAccountCommand> validator,
                                                 IBankAccountRepository repository,
                                                 ICustomerRepository customerRepository,
                                                 IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateBankAccountCommand, Result<CreateBankAccountResponse>>
    {
        private const string BRANCH_CODE = "001";

        public async Task<Result<CreateBankAccountResponse>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await Validate(request, cancellationToken);
            if (!validationResult.isValid)
                return Result<CreateBankAccountResponse>.Fail(validationResult.notifications);

            var bankAccount = await Create(request, cancellationToken);

            var response = new CreateBankAccountResponse(bankAccount.Id);
            return Result<CreateBankAccountResponse>.Successful(response);
        }

        private async Task<(bool isValid, List<string> notifications)> Validate(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            if (!await customerRepository.CustomerExists(request.CustomerId))
                return (false, new List<string> { Messages.CUSTOMER_NOT_FOUND });

            if (await repository.HasCustomerBankAccount(request.CustomerId))
                return (false, new List<string> { Messages.CUSTOMER_ALREADY_HAVE_BANKACCOUNT });

            return (true, new());
        }

        private async Task<Domain.Entities.BankAccount> Create(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var accountNumber = await GenerateAccountNumber();
            decimal initialFunds = 0;
            var bankAccount = new Domain.Entities.BankAccount(request.CustomerId, BRANCH_CODE, accountNumber, initialFunds, initialFunds, initialFunds);

            await repository.Add(bankAccount);
            await unitOfWork.Commit(cancellationToken);
            return bankAccount;
        }

        private async Task<string> GenerateAccountNumber()
        {
            var accountNumber = new Random().Next(10000000, 100000000).ToString();

            if (await repository.AccountNumberExists(accountNumber))
                return await GenerateAccountNumber();

            return accountNumber.ToString();
        }
    }
}
