using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labsit.Infrastructure.Configurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BranchCode).HasColumnName("BranchCode");
            builder.Property(x => x.AccountNumber).HasColumnName("AccountNumber");
            builder.Property(x => x.Balance).HasColumnName("Balance");

            builder.HasOne(b => b.Customer)
                .WithOne(c => c.BankAccount)
                .HasForeignKey<BankAccount>(b => b.CustomerId);
        }
    }
}
