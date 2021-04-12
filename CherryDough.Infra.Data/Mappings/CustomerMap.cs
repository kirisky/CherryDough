using CherryDough.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CherryDough.Infra.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.Id)
                .HasColumnName("Id");

            builder.Property(i => i.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.Description)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(i => i.Category)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}