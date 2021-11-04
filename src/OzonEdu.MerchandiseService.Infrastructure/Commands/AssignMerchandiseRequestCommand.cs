using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands
{
    public class AssignMerchandiseRequestCommand: IRequest<Manager>
    {
        public long EmployeeId { get;  init; }
        
        /// <summary>
        /// Коллекция мерча
        /// </summary>
        public int MerchType { get;  init; }
    }
}