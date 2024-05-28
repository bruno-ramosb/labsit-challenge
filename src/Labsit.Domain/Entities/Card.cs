using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labsit.Domain.Entities
{
    public class Card : AuditableEntity<int>
    {
        public Card(int bankAccountId, 
                    string number, 
                    string holderName, 
                    string verificationCode, 
                    ECardBrand brand, 
                    DateOnly expiryDate)
        {
            BankAccountId = bankAccountId;
            Number = number;
            HolderName = holderName;
            VerificationCode = verificationCode;
            Brand = brand;
            ExpiryDate = expiryDate;
        }
        public string Number { get; private set; }
        public string HolderName { get; private set; }
        public ECardBrand Brand { get; private set; }
        public DateOnly ExpiryDate { get; private set; }
        public string VerificationCode { get; private set; }
        public int BankAccountId { get; private set; }
        public BankAccount BankAccount { get; set; }
    }
}
