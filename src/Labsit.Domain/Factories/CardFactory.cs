using Labsit.Domain.Entities;
using Labsit.Domain.Enums;

namespace Labsit.Domain.Factories
{
    public class CardFactory
    {
        public static Card Create(int bankAccountId, string number, string holderName, string verificationCode, ECardBrand brand, DateOnly expiryDate) =>
            new( bankAccountId,  number,  holderName,  verificationCode,  brand,  expiryDate);
    }
}
