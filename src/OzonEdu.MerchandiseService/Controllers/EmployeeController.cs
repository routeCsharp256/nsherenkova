using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Infrastructure.Commands;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost ("/create")]
        public async Task<ActionResult> Create(EmployeeCreationModel employeeCreationModel, CancellationToken cancellationToken)
        {
            var createEmployee = new CreateEmployeeCommand()
            {
                FirstName = employeeCreationModel.FirstName,
                LastName = employeeCreationModel.LastName,
                Email = employeeCreationModel.Email,
                PhoneNumber = employeeCreationModel.PhoneNumber,
                Size = employeeCreationModel.Size
            };
            await _mediator.Send(createEmployee, cancellationToken);
            
            return Ok();
        }
    }
}