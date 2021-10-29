using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merchandise")]
    [Produces("application/json")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseController(IMerchService merchService)
        {
            _merchService = merchService;
        }
        
        /// <summary>
        /// получить информацию о выдаче мерча по id сотрудника
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<ActionResult<MerchItem>> MerchandiseResponse (long id, CancellationToken token)
        {
            var merchItem = await _merchService.MerchandiseResponse(id, token);
            if (merchItem is null)
            {
                return NotFound();
            }

            return merchItem;
            
        }

        /// <summary>
        /// запросить мерч
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MerchItem>>MerchandiseRequest (MerchItemRequest merchItemRequest, CancellationToken token)
        {
            var requestMerchItem = await _merchService.MerchandiseRequest(new MerchItemCreationModel
            {
                ItemName = merchItemRequest.ItemName,
                EmployeeId = merchItemRequest.EmployeeId
            }, token);
            return Ok(requestMerchItem);
        }
    }
}