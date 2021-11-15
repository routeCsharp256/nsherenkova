using System;
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
        public List<Sku> ListSku { get; }
        public RequestToReceiveMerchElementsDomainEvent(List<Sku> listSku)
        {
            ListSku = listSku?? throw new ArgumentNullException($"{nameof(listSku)} should not be null");
        }
    }
}