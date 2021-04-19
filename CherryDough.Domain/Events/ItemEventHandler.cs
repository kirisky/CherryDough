using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CherryDough.Domain.Events
{
    public class ItemEventHandler :
        INotificationHandler<AddedItemEvent>,
        INotificationHandler<UpdatedItemEvent>,
        INotificationHandler<RemovedItemEvent>
    {
        public Task Handle(AddedItemEvent notification, CancellationToken cancellationToken)
        {
            // Could send some notifications here.
            return Task.CompletedTask;

        }

        public Task Handle(UpdatedItemEvent notification, CancellationToken cancellationToken)
        {
            // Could send some notifications here.
            return Task.CompletedTask;
        }

        public Task Handle(RemovedItemEvent notification, CancellationToken cancellationToken)
        {
            // Could send some notifications here.
            return Task.CompletedTask;
        }
    }
}