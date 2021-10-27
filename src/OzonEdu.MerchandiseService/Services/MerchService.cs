using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Services
{
    public class MerchService : IMerchService
    {
        private readonly List<MerchItem> MerchItems = new List<MerchItem>();

        public Task<MerchItem> MerchandiseRequest(MerchItemCreationModel merchItem, CancellationToken _)
        {
            var newMerchItem = new MerchItem(merchItem.ItemName, merchItem.EmployeeId);
            MerchItems.Add(newMerchItem);
            return Task.FromResult(newMerchItem);
        }
        public Task<MerchItem> MerchandiseResponse(long employeeId, CancellationToken _)
        {
            var merchItem = MerchItems.FirstOrDefault(x => x.EmployeeId == employeeId);
            return Task.FromResult(merchItem);
        }
    }
}