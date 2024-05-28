using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labsit.Infrastructure.Configurations
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("CARD", "FINANCE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
                
            builder.Property(x => x.Number).HasColumnName("Number")
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(x => x.HolderName)
                .HasColumnName("HolderName")
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(x => x.Brand).HasColumnName("Brand").IsRequired();

            builder.Property(x => x.ExpiryDate).HasColumnName("ExpiryDate").IsRequired();

            builder.Property(x => x.VerificationCode).HasColumnName("VerificationCode").IsRequired();

            builder.HasOne(c => c.BankAccount)
                .WithMany(b => b.Cards)
                .HasForeignKey(c => c.BankAccountId);
        }
    }
}
