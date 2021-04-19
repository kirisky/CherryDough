using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CherryDough.Doamin.Core.Events;
using CherryDough.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CherryDough.Infra.Data.Repository.EventSourcing
{
    public class StoredStoredEventRepository : IStoredEventRepository
    {
        private readonly StoredEventContext _context;

        public StoredStoredEventRepository(StoredEventContext context)
        {
            _context = context;
        }
        
        public void Store(StoredEvent eventObj)
        {
            _context.StoredEvents.Add(eventObj);
            _context.SaveChanges();
        }

        public async Task<IList<StoredEvent>> All(Guid aggregateId)
        {
            return await 
                (from e in _context.StoredEvents where e.AggregateId == aggregateId select e)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}