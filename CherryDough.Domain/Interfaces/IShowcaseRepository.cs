using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Domain.Models;
using NetDevPack.Data;

namespace CherryDough.Domain.Interfaces
{
    public interface IShowcaseRepository : IRepository<Item>
    {
        Task<Item> GetById(Guid id);
        Task<Item> GetByName(string name);
        Task<IEnumerable<Item>> GetAll();
        void Add(Item item);
        void Update(Item item);
        void Remove(Item item);
    }
}