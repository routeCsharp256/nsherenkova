using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers
{
    public class AssignMerchandiseRequestCommandHandler: IRequestHandler<AssignMerchandiseRequestCommand, Manager>
    {
        private readonly IMerchandiseRequestRepository _merchandiseRequestRepository;
        
        private readonly IManagerRepository _managerRepository;

        public AssignMerchandiseRequestCommandHandler(IMerchandiseRequestRepository merchandiseRequestRepository, IManagerRepository managerRepository)
        {
            _merchandiseRequestRepository = merchandiseRequestRepository;
            _managerRepository = managerRepository;
        }
        public async Task<Manager> Handle(AssignMerchandiseRequestCommand request, CancellationToken cancellationToken)
        {
            var merchInDb = await _merchandiseRequestRepository
                .FindByIdAndMerchPackAsync(request.EmployeeId,
                    request.MerchType, cancellationToken);
            var responsibleManager = _managerRepository.FindManagerCanHandleNewTask(cancellationToken);
            if (responsibleManager is null)
            {
                throw new Exception("No vacant managers");
            }
            merchInDb.AssignTo(responsibleManager.Id);
            responsibleManager.Result.AssignTask();
            
            var createResult =
                await _merchandiseRequestRepository.CreateAsync(merchInDb, cancellationToken);
            await _merchandiseRequestRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            return responsibleManager.Result;
        }
    }
}