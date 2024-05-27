using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Entities;
using Labsit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Infrastructure.Repositories
{
    public class CustomerRepository(LabsitContext context) : BaseRepository<Customer, int>(context), ICustomerRepository
    {
        public async Task<bool> CustomerExists(int customerId) => 
            await context.Customers.AnyAsync(x => x.Id == customerId);

        public async Task<bool> IsDocumentInUse(string document) =>
         await context.Customers.AnyAsync(x => x.Document == document);
    }
}
