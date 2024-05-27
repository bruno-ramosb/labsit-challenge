using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Application.Features.Card.Responses.Query
{
    public class CardQueryModel : IQueryModel<int>
    {
        public CardQueryModel(int id, string number, string holderName, ECardBrand brand, DateOnly expiryDate, string verificationCode, int bankAccountId)
        {
            Id = id;
            Number = number;
            HolderName = holderName;
            Brand = brand;
            ExpiryDate = expiryDate;
            VerificationCode = verificationCode;
            BankAccountId = bankAccountId;
        }

        public int Id { get; private init; }
        public string Number { get; private init; }
        public string HolderName { get; private init; }
        public ECardBrand Brand { get; private init; }
        public DateOnly ExpiryDate { get; private init; }
        public string VerificationCode { get; private init; }
        public int BankAccountId { get; private init; }
    }
}
