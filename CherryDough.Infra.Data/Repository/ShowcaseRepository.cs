using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CherryDough.Domain.Interfaces;
using CherryDough.Domain.Models;
using CherryDough.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace CherryDough.Infra.Data.Repository
{
    public class ShowcaseRepository : IShowcaseRepository
    {
        protected readonly CherryDoughContext DbContext;

        public ShowcaseRepository(CherryDoughContext context)
        {
            DbContext = context;
        }

        public IUnitOfWork UnitOfWork => DbContext;

        public async Task<Item> GetById(Guid id)
        {
            return await DbContext.Items.FindAsync(id);
        }

        public async Task<Item> GetByName(string name)
        {
            return await DbContext.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await DbContext.Items.ToListAsync();
        }
        
        public void Add(Item item)
        {
            DbContext.Items.Add(item);
        }

        public void Update(Item item)
        {
            DbContext.Items.Update(item);
        }

        public void Remove (Item item)
        {
            DbContext.Remove(item);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}