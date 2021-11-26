using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Implementation
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly List<Manager> _items = new List<Manager>()
        { 
            new Manager(new PersonName("FirstName2", "LastName2"), new Email("a2@mail.ru"), 0)
        }; 
        public IUnitOfWork UnitOfWork { get; }
        public Task<Manager> CreateAsync(Manager itemToCreate, CancellationToken cancellationToken = default)
        {
            _items.Add(itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<Manager> UpdateAsync(Manager itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Manager> FindManagerCanHandleNewTask( CancellationToken cancellationToken = default)
        {
            var managerItem = _items.FirstOrDefault(x => x.CanHandleNewTask);
            return Task.FromResult(managerItem);
        }
        public Task<Manager> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var managerItem = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(managerItem);
        }

       
    }
}