using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.Commands;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers
{
    public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(CreateEmployeeCommand employeeCommand, CancellationToken cancellationToken)
        {
            var newEmployee = Employee.Create(
                new PersonName(employeeCommand.FirstName, employeeCommand.LastName),
                new Email(employeeCommand.Email),
                new PhoneNumber(employeeCommand.PhoneNumber),
                ClothingSize.Parse(employeeCommand.Size));
            
            var createResult =
                await _employeeRepository.CreateAsync(newEmployee, cancellationToken);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}