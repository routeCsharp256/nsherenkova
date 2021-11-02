using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchType: Enumeration
    {
        public static MerchType WelcomePack = new(1, nameof(WelcomePack));
        public static MerchType StarterPack  = new(2, nameof(StarterPack));
        public static MerchType ConferenceListenerPack  = new(3, nameof(ConferenceListenerPack));
        public static MerchType ConferenceSpeakerPack = new(4, nameof(ConferenceSpeakerPack));
        public static MerchType VeteranPack  = new(5, nameof(VeteranPack));
        
        public MerchType(int id, string name) : base(id, name)
        {
        }
    }
}