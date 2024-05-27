using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Infrastructure.Context
{
    public class LabsitContext : DbContext
    {
        public LabsitContext(DbContextOptions<LabsitContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabsitContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = entry.Entity.CreatedAt;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in ChangeTracker.Entries().ToList())
                entry.State = EntityState.Detached;

            return result;
        }
    }
}

