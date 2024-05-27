using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Handlers
{
    public class DepositCommandHandler(IValidator<DepositCommand> validator,
                                       IBankAccountRepository bankAccountRepository, 
                                       IUnitOfWork unitOfWork) : IRequestHandler<DepositCommand, Result<DepositResponse>>
    {
        public async Task<Result<DepositResponse>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<DepositResponse>.Fail(validationResult.Errors);

            var bankAccount = await bankAccountRepository.GetByIdAsync(request.BankAccountId);
            if(bankAccount is null)
                return Result<DepositResponse>.Fail(Messages.BANK_ACCOUNT_NOT_FOUND);

            switch (request.TransactionType)
            {
                case ETransactionType.Debit:
                    bankAccount.AddBalance(request.Value);
                    break;
                case ETransactionType.Credit:
                    bankAccount.AddTotalCreditLimit(request.Value);
                    bankAccount.AddAvailableCreditLimit(request.Value);
                    break;
            }

            await bankAccountRepository.Update(bankAccount);

            await unitOfWork.Commit(cancellationToken);

            var response = new DepositResponse(bankAccount.Id, bankAccount.Balance, bankAccount.TotalCreditLimit, bankAccount.AvailableCreditLimit);

            return Result<DepositResponse>.Successful(response);
        }
    }
}
