using Labsit.Domain.Entities;

namespace Labsit.Domain.Contracts.Repositories
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount, int>
    {
        Task<bool> HasCustomerBankAccount(int customerId);
        Task<bool> AccountNumberExists(string accountNumber);
    }
}
