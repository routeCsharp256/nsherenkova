namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.ModelsDb
{
    public class MerchandiseItemDb
    {
        public int? Id { get; set; }
        public int MerchPackId { get; set; }
        public int[] Items { get; set; }
    }
}