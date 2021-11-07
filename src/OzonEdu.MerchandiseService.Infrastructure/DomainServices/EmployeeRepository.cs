using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _items = new List<Employee>()
        { 
            new Employee(
                new PersonName("FirstName1", "LastName1"), 
                new Email("a1@mail.ru"), ClothingSize.S, 
                new PhoneNumber("88888888888"))
        }; 
        public IUnitOfWork UnitOfWork { get; }
        public Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var employeeItem = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(employeeItem);
        }
    }
}