using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Entities;
using Labsit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Infrastructure.Repositories
{
    public class BankAccountRepository(LabsitContext context) : BaseRepository<BankAccount, int>(context), IBankAccountRepository
    {
        public async Task<bool> HasCustomerBankAccount(int customerId) => 
            await context.BankAccounts.AnyAsync(x => x.CustomerId == customerId);
        
        public async Task<bool> AccountNumberExists(string accountNumber) => 
            await context.BankAccounts.AnyAsync(x => x.AccountNumber == accountNumber);
    }
}
