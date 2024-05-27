using Labsit.Domain.Contracts.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labsit.Domain.Entities
{
    [Table("BankAccount", Schema = "Finance")]
    public class BankAccount : AuditableEntity<int>
    {
        public BankAccount(int customerId,
            string branchCode,
            string accountNumber,
            decimal balance,
            decimal totalCreditLimit,
            decimal availableCreditLimit)
        {
            CustomerId = customerId;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            Balance = balance;
            TotalCreditLimit = totalCreditLimit;
            AvailableCreditLimit = availableCreditLimit;
        }

        [Required]
        public string BranchCode { get; private set; }

        [Required]
        public string AccountNumber { get; private set; }

        [Required]
        public decimal Balance { get; private set; }

        [Required]
        public decimal TotalCreditLimit { get; private set; }

        [Required]
        public decimal AvailableCreditLimit { get; private set; }

        [Required]
        [ForeignKey("CustomerId")]
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
