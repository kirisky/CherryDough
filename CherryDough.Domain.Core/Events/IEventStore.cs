using NetDevPack.Messaging;

namespace CherryDough.Doamin.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T eventObj) where T : Event;
    }
}