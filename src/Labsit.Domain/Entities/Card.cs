using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Enums;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [MaxLength(16)]
        public string Number { get; private set; }

        [MaxLength(80)]
        public string HolderName { get; private set; }

        [Required]
        public ECardBrand Brand { get; private set; }

        [Required]
        public DateOnly ExpiryDate { get; private set; }

        [MaxLength(3)]
        public string VerificationCode { get; private set; }

        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; private set; }
        public BankAccount BankAccount { get; set; }
    }
}
