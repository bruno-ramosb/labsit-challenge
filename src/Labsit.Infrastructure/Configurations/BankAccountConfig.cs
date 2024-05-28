using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labsit.Infrastructure.Configurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BANKACCOUNT", "FINANCE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.BranchCode).HasColumnName("BRANCHCODE").IsRequired();
            builder.Property(x => x.AccountNumber).HasColumnName("ACCOUNTNUMBER").IsRequired();
            builder.Property(x => x.Balance).HasColumnName("BALANCE").IsRequired();
            builder.Property(x => x.TotalCreditLimit).HasColumnName("TOTALCREDITLIMIT").IsRequired();
            builder.Property(x => x.AvailableCreditLimit).HasColumnName("AVAILABLECREDITLIMIT").IsRequired();
            builder.Property(x => x.CustomerId).HasColumnName("IDCUSTOMER").IsRequired();

            builder.HasOne(b => b.Customer)
                .WithOne(c => c.BankAccount)
                .HasForeignKey<BankAccount>(b => b.CustomerId);
        }
    }
}
