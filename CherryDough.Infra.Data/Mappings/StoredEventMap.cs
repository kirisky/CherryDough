using CherryDough.Doamin.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CherryDough.Infra.Data.Mappings
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(e => e.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(e => e.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}