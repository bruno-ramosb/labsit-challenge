using Labsit.Application.Common.Response;

namespace Labsit.Application.Features.BankAccount.Responses
{
    public class DepositResponse(int bankAccountId,decimal balance,decimal totalCreditLimit,decimal availableCreditLimit) : IResponse
    {
        public int BankAccountId { get; } = bankAccountId;
        public decimal Balance { get; } = balance;
        public decimal TotalCreditLimit { get; } = totalCreditLimit;
        public decimal AvailableCreditLimit { get; } = availableCreditLimit;
    }
}
