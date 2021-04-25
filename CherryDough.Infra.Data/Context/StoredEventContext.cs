using CherryDough.Doamin.Core.Events;
using CherryDough.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CherryDough.Infra.Data.Context
{
    public class StoredEventContext : DbContext
    {
        public StoredEventContext(DbContextOptions<StoredEventContext> options)
            : base(options)
        {
            
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}