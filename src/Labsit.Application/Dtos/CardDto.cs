using Labsit.Domain.Enums;

namespace Labsit.Application.Dtos
{
    public class CardDto
    {
        public CardDto(string number, string holderName, ECardBrand brand, DateOnly expiryDate, string verificationCode, ETransactionType transactionType)
        {
            Number = number;
            HolderName = holderName;
            Brand = brand;
            ExpiryDate = expiryDate;
            VerificationCode = verificationCode;
            TransactionType = transactionType;
        }
        public CardDto() { }

        public string Number { get; private set; }
        public string HolderName { get; private set; }
        public ECardBrand Brand { get; private set; }
        public DateOnly ExpiryDate { get; private set; }
        public string VerificationCode { get; private set; }
        public ETransactionType TransactionType { get; private set; }
    }
}
