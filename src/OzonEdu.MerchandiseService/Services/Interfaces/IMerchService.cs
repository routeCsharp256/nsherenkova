using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
namespace OzonEdu.MerchandiseService.Services.Interfaces
{
    public interface IMerchService
    {
        Task<MerchItem> MerchandiseRequest(MerchItemCreationModel merchItem, CancellationToken _);
        Task<MerchItem> MerchandiseResponse(long employeeId, CancellationToken _);
    }
}