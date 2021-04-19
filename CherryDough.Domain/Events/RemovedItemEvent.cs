using System;
using NetDevPack.Messaging;

namespace CherryDough.Domain.Events
{
    public class RemovedItemEvent : Event
    {
        public RemovedItemEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}