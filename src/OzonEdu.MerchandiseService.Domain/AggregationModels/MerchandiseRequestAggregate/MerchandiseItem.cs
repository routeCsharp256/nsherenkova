using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseItem : Entity
    {
        public Type MerchType { get;  }
        public List<Sku> Items { get; private set; }
    }
}