using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;
using OzonEdu.MerchandiseService.Infrastructure.Commands;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers
{
    public class CreateMerchandiseRequestCommandHandler : IRequestHandler<CreateMerchandiseRequestCommand>
    {
        private readonly IMerchandiseRequestRepository _merchandiseRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateMerchandiseRequestCommandHandler(IMerchandiseRequestRepository merchandiseRequestRepository,
            IEmployeeRepository employeeRepository)
        {
            _merchandiseRequestRepository = merchandiseRequestRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(CreateMerchandiseRequestCommand request, CancellationToken cancellationToken)
        {
            var merchInDb = await _merchandiseRequestRepository
                .FindByIdAndMerchPackAsync(request.EmployeeId,
                request.MerchType, cancellationToken);
            if (merchInDb is not null)
                throw new Exception(
                    $"MerchandiseRequest with EmployeeId {request.EmployeeId} and {request.MerchType} already exist");

            var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
            var newMerchandiseRequest = new MerchandiseRequest(request.EmployeeId, employee.PhoneNumber);
            var merchPack = new MerchPack(MerchType.GetAll<MerchType>()
                .FirstOrDefault(it => it.Id.Equals(request.MerchType)));
            newMerchandiseRequest.Create(merchPack);

            var createResult =
                await _merchandiseRequestRepository.CreateAsync(newMerchandiseRequest, cancellationToken);
            await _merchandiseRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}

   
