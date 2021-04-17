using System;
using NetDevPack.Messaging;

namespace CherryDough.Domain.Commands
{
    public abstract class ItemCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Category { get; protected set; }
    }
}