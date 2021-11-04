using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces
{
    public interface IManagerRepository: IRepository<Manager>, IEnumerable<Manager>
    {
        /// <summary>
        /// Найти Мэнеджера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Мэнеджера</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Мэнеджер</returns>
        Task<Manager> FindByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}