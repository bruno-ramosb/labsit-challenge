using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Factories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Application.Features.BankAccount.Handlers
{
    public class CreateBankAccountCommandHandler(IValidator<CreateBankAccountCommand> validator,
                                                 IBankAccountRepository repository,
                                                 ICustomerRepository customerRepository,
                                                 IUnitOfWork unitOfWork) : IRequestHandler<CreateBankAccountCommand, Result<CreateBankAccountResponse>>
    {
        private const string BRANCH_CODE = "001";

        public async Task<Result<CreateBankAccountResponse>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<CreateBankAccountResponse>.Fail(validationResult.Errors);

            if (!await customerRepository.CustomerExists(request.CustomerId))
               return Result< CreateBankAccountResponse>.Fail(Messages.CUSTOMER_NOT_FOUND);

            if (await repository.HasCustomerBankAccount(request.CustomerId))
                return Result<CreateBankAccountResponse>.Fail(Messages.CUSTOMER_ALREADY_HAVE_BANKACCOUNT);

            var accountNumber = await GenerateAccountNumber();

            var bankAccount = BankAccountFactory.Create(request.CustomerId, BRANCH_CODE, accountNumber);
            bankAccount.AddBalance(0);

            await repository.Add(bankAccount);

            await unitOfWork.Commit(cancellationToken);

            var response = new CreateBankAccountResponse(bankAccount.Id);

            return Result<CreateBankAccountResponse>.Successful(response);
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
