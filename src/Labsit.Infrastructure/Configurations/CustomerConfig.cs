using Labsit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labsit.Infrastructure.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMER", "FINANCE");

            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id).HasColumnName("Id");

            builder.Property(x=>x.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x=>x.Document).HasColumnName("Document")
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(x=>x.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .IsRequired();
        }
    }
}
