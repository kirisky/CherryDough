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

        public IUnitOfWork UnitOfWork { get; }

        public async Task<Item> GetById(Guid id)
        {
            return await DbContext.Items.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await DbContext.Items.ToListAsync();
        }

        public async Task<bool> Add(Item item)
        {
            await DbContext.Items.AddAsync(item);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Item item)
        {
            DbContext.Items.Update(item);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove (Item item)
        {
            DbContext.Remove(item);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}