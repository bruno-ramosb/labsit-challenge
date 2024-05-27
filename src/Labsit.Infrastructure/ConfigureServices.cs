using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Entities;
using Labsit.Infrastructure.Context;

namespace Labsit.Infrastructure
{
    public class DbSeeder : IDbSeeder
    {
        private readonly LabsitContext _context;

        public DbSeeder(LabsitContext context)
        {
            _context = context;
        }

        public void InitializeAsync()
        {
            _context.Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
            var customer = new Customer("Roberto Santos Andrade", "57208611068", DateOnly.FromDateTime(DateTime.Today.AddYears(-18)));
            _context.Customers.Add(customer);

            var bankAccount = new BankAccount(1, "001", "12345678", 7000, 7000, 7000);
            _context.BankAccounts.Add(bankAccount);

            var card = new Card(1, "5192858385719792", "Roberto S A", "123", Domain.Enums.ECardBrand.Visa, DateOnly.Parse("2040-01-01"));
            _context.Cards.Add(card);

            _context.SaveChanges();
        }
    }
}
