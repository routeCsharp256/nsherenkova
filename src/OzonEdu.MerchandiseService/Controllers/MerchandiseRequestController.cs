using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Infrastructure.Commands;

namespace OzonEdu.MerchandiseService.Controllers
{
    
    [ApiController]
    [Route("v1/api/merch/mediatr")]
    public class MerchandiseRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public MerchandiseRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMerchRequest(long employeeId, int merchType, CancellationToken cancellationToken)
        {
            var createMerchRequest = new CreateMerchandiseRequestCommand()
            {
                EmployeeId = employeeId,
                MerchType = merchType
            };
            await _mediator.Send(createMerchRequest, cancellationToken);
            
            return Ok();
        }
        [HttpGet("assign/{employeeId:long}/{merchType:int}")]
        public async Task<ActionResult> AssignMerchRequest(long employeeId, int merchType, CancellationToken cancellationToken)
        {
            var assignMerchRequest = new AssignMerchandiseRequestCommand()
            {
                EmployeeId = employeeId,
                MerchType = merchType
            };
            var result = await _mediator.Send(assignMerchRequest, cancellationToken);
            
            return Ok(result);
        }
    }
}