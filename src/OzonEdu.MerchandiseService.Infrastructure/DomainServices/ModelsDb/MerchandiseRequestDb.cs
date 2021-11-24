using System.Diagnostics.CodeAnalysis;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.ModelsDb
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MerchandiseRequestDb
    {
        public int Id { get; set; }
        public MerchandiseRequestStatus Status { get; set; }
        public long EmployeeId { get; set; }
        public long? MenagerId { get; set; }
        public long? MerchandiseItemId { get; set; }
    }
}