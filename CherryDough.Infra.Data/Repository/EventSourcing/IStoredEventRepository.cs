using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Doamin.Core.Events;

namespace CherryDough.Infra.Data.Repository.EventSourcing
{
    public interface IStoredEventRepository : IDisposable
    {
        void Store(StoredEvent eventObj);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}