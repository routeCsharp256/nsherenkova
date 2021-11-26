using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces
{
    public interface IMerchandiseRequestRepository: IRepository<MerchandiseRequest>
    {
        /// <summary>
        /// Найти MerchandiseRequest по идентификатору сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Запрос на мерч</returns>
        Task<MerchandiseRequest> FindByEmployeeIdAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Найти MerchandiseRequest по идентификатору сотрудника и названию пакета мерча
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// /// <param name="merchPack">Идентификатор типа мерча</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Запрос на мерч</returns>
        Task<MerchandiseRequest> FindByEmployeeIdAndMerchPackAsync(long id, int merchPack, CancellationToken cancellationToken = default);
    }
}