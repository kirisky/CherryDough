using CherryDough.Doamin.Core.Events;
using CherryDough.Infra.Data.Repository.EventSourcing;
using NetDevPack.Identity.User;
using Newtonsoft.Json;

namespace CherryDough.Infra.Data.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IStoredEventRepository _storedEventRepository;
        private readonly IAspNetUser _user;

        public EventStore(IStoredEventRepository storedEventRepository, IAspNetUser user)
        {
            _storedEventRepository = storedEventRepository;
            _user = user;
        }
        public void Save<T>(T eventObj) where T : NetDevPack.Messaging.Event
        {
            var serializedEvent = JsonConvert.SerializeObject(eventObj);

            var storedEvent = new StoredEvent(eventObj, serializedEvent, _user.Name ?? _user.GetUserEmail());
            
            _storedEventRepository.Store(storedEvent);
        }
    }
}