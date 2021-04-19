using System;
using NetDevPack.Messaging;

namespace CherryDough.Domain.Events
{
    public class AddedItemEvent : Event
    {
        public AddedItemEvent(Guid id, string name, string description, string category)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
    }
}