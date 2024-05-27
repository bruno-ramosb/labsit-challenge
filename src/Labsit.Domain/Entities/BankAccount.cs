using Labsit.Domain.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labsit.Domain.Entities
{
    [Table("BankAccount", Schema = "Finance")]
    public class BankAccount : AuditableEntity<int>
    {
        public BankAccount(int customerId,string branchCode,string accountNumber)
        {
            CustomerId = customerId;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
        }

        public string BranchCode { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public decimal TotalCreditLimit { get; private set; }
        public decimal AvailableCreditLimit { get; private set; }
        public int CustomerId { get; private set; }

        public Customer Customer { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public void AddBalance(decimal value) =>
            Balance += value;

        public void AddTotalCreditLimit(decimal value) =>
            TotalCreditLimit += value;

        public void AddAvailableCreditLimit(decimal value) =>
            AvailableCreditLimit += value;

        public void WithdrawBalance(decimal value) => 
            Balance -= value;

        public void WithdrawAvailableCredit(decimal value) =>
            AvailableCreditLimit -= value;
    }
}
