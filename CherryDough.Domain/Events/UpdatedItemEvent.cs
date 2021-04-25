using System;
using NetDevPack.Messaging;

namespace CherryDough.Domain.Events
{
    public class UpdatedItemEvent : Event
    {
        public UpdatedItemEvent(Guid id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
    }
}