using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Entities;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Infrastructure.Repositories
{
    public class CardRepository(LabsitContext context) : BaseRepository<Card, int>(context), ICardRepository
    {
        public async Task<Card> GetValidCard(string number, string holderName, string verificationCode, ECardBrand brand, DateOnly expiryDate)
        {
            return await context.Cards
            .AsNoTracking()
            .Where(x => x.Number == number &&
            x.HolderName.ToUpper() == holderName.ToUpper() &&
            x.VerificationCode == verificationCode &&
            x.Brand == brand)
            .FirstOrDefaultAsync();
        }

        public async Task<bool> HasCardInBankAccount(int bankAccountId) =>
            await context.Cards.AsNoTracking().AnyAsync(x => x.BankAccountId == bankAccountId);
    }
}
