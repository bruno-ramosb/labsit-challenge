using Labsit.Domain.Entities;
using Labsit.Infrastructure.Context;
using Labsit.Test._Builders;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Test.Fixtures
{
    public class EfSqliteFixture : IAsyncLifetime, IDisposable
    {
        private const string ConnectionString = "Data Source=:memory:";
        private readonly SqliteConnection _connection;

        public EfSqliteFixture()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();

            var builder = new DbContextOptionsBuilder<LabsitContext>().UseSqlite(_connection);
            Context = new LabsitContext(builder.Options);
        }

        public LabsitContext Context { get; }

        #region IAsyncLifetime

        public async Task InitializeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();

            await Seed();
        }

        private async Task Seed()
        {
            foreach (var customer in GetValidCustomers())
            {
                Context.Customers.Add(customer);
                await Context.SaveChangesAsync();
            }

            foreach (var bankAccount in GetValidBankAccount())
            {
                Context.BankAccounts.Add(bankAccount);
                await Context.SaveChangesAsync();
            }

            foreach (var card in GetValidCards())
            {
                Context.Cards.Add(card);
                await Context.SaveChangesAsync();
            }
        }

        public List<Card> GetValidCards()
        {
            var cards = new List<Card>
            {
                new CardBuilder().New().WithFunds().BuildCard(),
                new CardBuilder().New().WithoutFunds().BuildCard(),
            };
            return cards;
        }

        public List<Customer> GetValidCustomers()
        {
            var customer = new List<Customer>
            {
                new Customer("Roberto Santos Andrade", "57208611068", DateOnly.FromDateTime(DateTime.Today.AddYears(-18))),
                new Customer("Maria Roberta Almeida", "25363399040",DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
            };
            return customer;
        }

        private static List<BankAccount> GetValidBankAccount()
        {
            return new List<BankAccount>
            {
                new BankAccountBuilder().New().WithFunds().Build(),
                new BankAccountBuilder().New().WithoutFunds().Build()
            };
        }

        public Task DisposeAsync() => Task.CompletedTask;

        #endregion

        #region IDisposable

        private bool _disposed;

        ~EfSqliteFixture() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _connection?.Dispose();
                Context?.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
