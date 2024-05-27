using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Commands
{
    public class WithdrawCommand : IRequest<Result<WithdrawResponse>>
    {
        public WithdrawCommand(int bankAccountId, decimal value, ETransactionType transactionType)
        {
            BankAccountId = bankAccountId;
            Value = value;
            TransactionType = transactionType;
        }
        public WithdrawCommand() { }

        public int BankAccountId { get; private set; }
        public decimal Value { get; private set; }
        public ETransactionType TransactionType { get; private set; }
    }
}
