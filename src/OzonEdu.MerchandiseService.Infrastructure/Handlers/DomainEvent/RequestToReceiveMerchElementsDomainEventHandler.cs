using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class RequestToReceiveMerchElementsDomainEventHandler: INotificationHandler<RequestToReceiveMerchElementsDomainEvent>
    {
        public Task Handle(RequestToReceiveMerchElementsDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}