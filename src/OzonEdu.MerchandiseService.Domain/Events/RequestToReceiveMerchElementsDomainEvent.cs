using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    /// <summary>
    /// запрос на получение элементов мерча
    /// </summary>
    public class RequestToReceiveMerchElementsDomainEvent : INotification
    {
        public RequestToReceiveMerchElementsDomainEvent(List<Sku> listSku)
        {
            ListSku = listSku;
        }

        public List<Sku> ListSku { get; }

    }
}