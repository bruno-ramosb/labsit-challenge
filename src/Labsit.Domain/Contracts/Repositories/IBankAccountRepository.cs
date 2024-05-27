using Labsit.Domain.Entities;
using Labsit.Infrastructure.Repositories;

namespace Labsit.Domain.Contracts.Repositories
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount, int>
    {
        Task<bool> HasCustomerBankAccount(int customerId);
        Task<bool> AccountNumberExists(string accountNumber);
    }
}
