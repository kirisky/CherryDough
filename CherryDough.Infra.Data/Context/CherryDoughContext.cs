using System.Threading.Tasks;
using CherryDough.Domain.Models;
using CherryDough.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;

namespace CherryDough.Infra.Data.Context
{
    public class CherryDoughContext : DbContext, IUnitOfWork
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

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;
            return success;
        }
    }
}