using Labsit.Domain.Entities;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;

namespace Labsit.Domain.Contracts.Repositories
{
    public interface ICardRepository : IBaseRepository<Card,int>
    {
        Task<bool> HasCardInBankAccount(int bankAccountId);   
        Task<Card> GetValidCard(string number,string holderName,string verificationCode,ECardBrand brand,DateOnly expiryDate);   
    }
}
