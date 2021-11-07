using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchandiseRequestRepository: IMerchandiseRequestRepository
    {
        private readonly List<MerchandiseRequest> _items = new List<MerchandiseRequest>()
        { new MerchandiseRequest(1, new PhoneNumber("88888888888")),
            new MerchandiseRequest(2,new PhoneNumber("89123456789"))
        }; 
        public Task<MerchandiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var merchandiseRequestItem = _items.FirstOrDefault(x => x.EmployeeId == id);
            return Task.FromResult(merchandiseRequestItem);
        }

        public Task<MerchandiseRequest> FindByIdAndMerchPackAsync(long id, int merchPack, CancellationToken cancellationToken = default)
        {
            var merchandiseRequestItem = _items.FirstOrDefault(x => x.EmployeeId == id 
                                                                    && x.MerchandiseItem.MerchPack.MerchType.Id == merchPack);
            return Task.FromResult(merchandiseRequestItem);
        }

        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchandiseRequest> CreateAsync(MerchandiseRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchandiseRequest> UpdateAsync(MerchandiseRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}