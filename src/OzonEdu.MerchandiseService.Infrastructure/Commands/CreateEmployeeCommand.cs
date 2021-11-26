using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands
{
    public class CreateEmployeeCommand : IRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init;}
        public string Email { get; init;}
        public string PhoneNumber { get; init;}
        public int Size { get; init;}
    }
}