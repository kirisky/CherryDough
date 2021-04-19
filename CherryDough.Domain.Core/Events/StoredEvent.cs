using System;
using NetDevPack.Messaging;

namespace CherryDough.Doamin.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event eventObj, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = eventObj.AggregateId;
            MessageType = eventObj.MessageType;
            Data = data;
            User = user;
        }

        // For EFCore
        protected StoredEvent()
        {
            
        }

        public string User { get; private set; }

        public string Data { get; private set; }

        public Guid Id { get; private set; }
    }
}