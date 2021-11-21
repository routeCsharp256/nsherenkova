using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Implementation
{
    public class MerchandiseRequestRepository: IMerchandiseRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public MerchandiseRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker, IUnitOfWork unitOfWork)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            UnitOfWork = unitOfWork;
        }

        public Task<MerchandiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchandiseRequest> FindByIdAndMerchPackAsync(long id, int merchPack, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        
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