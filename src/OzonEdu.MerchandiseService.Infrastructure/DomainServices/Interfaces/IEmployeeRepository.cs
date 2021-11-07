using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;
namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        /// <summary>
        /// Найти Сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Сотрудник</returns>
        Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}