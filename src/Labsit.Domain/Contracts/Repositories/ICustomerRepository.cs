using Labsit.Domain.Entities;

namespace Labsit.Domain.Contracts.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer,int>
    {
        Task<bool> CustomerExists(int customerId);
        Task<bool> IsDocumentInUse(string document);
    }
}
