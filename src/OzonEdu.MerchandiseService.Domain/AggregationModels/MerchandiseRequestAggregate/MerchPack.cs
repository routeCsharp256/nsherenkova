using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchPack : Entity
    {
        public MerchType MerchType { get; }

        public MerchPack(MerchType type)
        {
            MerchType = type;
        }
    }
}