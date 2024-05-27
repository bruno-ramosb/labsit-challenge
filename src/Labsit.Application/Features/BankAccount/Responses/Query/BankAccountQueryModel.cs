using Labsit.Domain.Contracts.Entities;

namespace Labsit.Application.Features.BankAccount.Responses.Query
{
    public class BankAccountQueryModel : IQueryModel<int>
    {
        public BankAccountQueryModel(int id, string branchCode, string accountNumber, decimal balance, decimal totalCreditLimit, decimal availableCreditLimit, int customerId)
        {
            Id = id;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            Balance = balance;
            TotalCreditLimit = totalCreditLimit;
            AvailableCreditLimit = availableCreditLimit;
            CustomerId = customerId;
        }

        public int Id { get; private init; }
        public string BranchCode { get; private init; }
        public string AccountNumber { get; private init; }
        public decimal Balance { get; private init; }
        public decimal TotalCreditLimit { get; private init; }
        public decimal AvailableCreditLimit { get; private init; }
        public int CustomerId { get; private init; }
    }
}
