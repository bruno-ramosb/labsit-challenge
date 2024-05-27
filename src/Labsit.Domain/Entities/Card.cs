using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labsit.Domain.Entities
{
    [Table("Card", Schema = "Finance")]
    public class Card : AuditableEntity<int>
    {
        public Card(int bankAccountId, string number, string holderName, string verificationCode, ECardBrand brand, DateOnly expiryDate)
        {
            BankAccountId = bankAccountId;
            Number = number;
            HolderName = holderName;
            VerificationCode = verificationCode;
            Brand = brand;
            ExpiryDate = expiryDate;
        }

        public string Number { get; set; }
        public string HolderName { get; set; }
        public ECardBrand Brand { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string VerificationCode { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
