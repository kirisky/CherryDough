using System;
using NetDevPack.Domain;

namespace CherryDough.Domain.Models
{
    public class Item : Entity, IAggregateRoot
    {
        public Item(Guid id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; set; }
    }
}