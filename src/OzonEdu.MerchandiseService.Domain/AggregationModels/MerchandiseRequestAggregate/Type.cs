using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class Type : Entity
    {
        public MerchType MerchType { get; }

        public Type(MerchType type)
        {
            MerchType = type;
        }
    }
}