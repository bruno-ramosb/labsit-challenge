using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labsit.Infrastructure.Configurations
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Number).HasColumnName("Number");
            builder.Property(x => x.HolderName).HasColumnName("HolderName");
            builder.Property(x => x.Brand).HasColumnName("Brand");
            builder.Property(x => x.ExpiryDate).HasColumnName("ExpiryDate");
            builder.Property(x => x.VerificationCode).HasColumnName("VerificationCode");

            builder.HasOne(c => c.BankAccount)
                .WithMany(b => b.Cards)
                .HasForeignKey(c => c.BankAccountId);
        }
    }
}
