using CherryDough.Domain.Models;
using CherryDough.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Messaging;

namespace CherryDough.Infra.Data.Context
{
    public class CherryDoughContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        
        public CherryDoughContext(DbContextOptions<CherryDoughContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            
            modelBuilder.ApplyConfiguration(new CustomerMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}