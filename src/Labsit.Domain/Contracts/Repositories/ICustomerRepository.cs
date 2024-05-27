using Labsit.Domain.Entities;
using Labsit.Infrastructure.Repositories;

namespace Labsit.Domain.Contracts.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer,int>
    {
        Task<bool> CustomerExists(int customerId);
        Task<bool> IsDocumentInUse(string document);
    }
}
