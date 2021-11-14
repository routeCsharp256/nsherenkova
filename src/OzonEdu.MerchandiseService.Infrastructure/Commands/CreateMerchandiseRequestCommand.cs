using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands
{
    public class CreateMerchandiseRequestCommand : IRequest
    {
        public long EmployeeId { get;  init; }
        
        /// <summary>
        /// Тип мерча
        /// </summary>
        public int MerchType { get;  init; }
    }
}