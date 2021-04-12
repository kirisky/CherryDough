using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Domain.Models;
using NetDevPack.Data;

namespace CherryDough.Domain.Interfaces
{
    public interface IShowcaseRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetByCategory(string categoryName);
        Task<IEnumerable<Item>> GetAll();
        Task<bool> Add(Item item);
        Task<bool> Update(Item item);
        Task<bool> Remove(Item item);
    }
}