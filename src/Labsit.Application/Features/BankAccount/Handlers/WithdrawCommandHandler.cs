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
    public class WithdrawCommandHandler(IValidator<WithdrawCommand> validator,
                                        IBankAccountRepository bankAccountRepository, 
                                        IUnitOfWork unitOfWork) : IRequestHandler<WithdrawCommand, Result<WithdrawResponse>>
    {
        public async Task<Result<WithdrawResponse>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<WithdrawResponse>.Fail(validationResult.Errors);

            var bankAccount = await bankAccountRepository.GetByIdAsync(request.BankAccountId);
            if(bankAccount is null)
                return Result<WithdrawResponse>.Fail(Messages.BANK_ACCOUNT_NOT_FOUND);

            var withdraw = await Withdraw(bankAccount, request,cancellationToken);
            if(!withdraw.isValid)
                return Result<WithdrawResponse>.Fail(withdraw.notifications);

            var response = new WithdrawResponse(bankAccount.Id, bankAccount.Balance, bankAccount.TotalCreditLimit, bankAccount.AvailableCreditLimit);

            return Result<WithdrawResponse>.Successful(response);
        }

        private async Task<(bool isValid, List<string> notifications)> Withdraw(Domain.Entities.BankAccount bankAccount, WithdrawCommand request,CancellationToken cancellationToken)
        {
            switch (request.TransactionType)
            {
                case ETransactionType.Debit:
                    bankAccount.WithdrawBalance(request.Value);
                    break;
                case ETransactionType.Credit:
                    bankAccount.WithdrawAvailableCredit(request.Value);
                    break;
            }

            if (bankAccount.Balance < 0 || bankAccount.AvailableCreditLimit < 0)
                return (false, new List<string>() { Messages.INSUFFICIENT_FUNDS });

            await bankAccountRepository.Update(bankAccount);
            await unitOfWork.Commit(cancellationToken);

            return (true, new());
        }

    }
}
