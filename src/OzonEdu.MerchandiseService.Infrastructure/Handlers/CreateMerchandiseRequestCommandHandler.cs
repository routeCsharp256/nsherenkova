﻿using System;
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
            var merchInMerchandiseRequestRepository = await _merchandiseRequestRepository
                .FindByIdAndMerchPackAsync(request.EmployeeId,
                    request.MerchType, cancellationToken);
            if (merchInMerchandiseRequestRepository is not null)
                throw new Exception(
                    $"MerchandiseRequest with EmployeeId {request.EmployeeId} and {request.MerchType} already exist");
            MerchandiseRequest newMerchandiseRequest;
            var employeeInMerchandiseRequestRepository = await _merchandiseRequestRepository
                .FindByIdAsync(request.EmployeeId, cancellationToken);
            if (employeeInMerchandiseRequestRepository is not null)
                newMerchandiseRequest = employeeInMerchandiseRequestRepository;
            else
            {
                var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
                newMerchandiseRequest = new MerchandiseRequest(request.EmployeeId, employee.PhoneNumber);
            }

            var merchPack = new MerchPack(MerchType.GetAll<MerchType>()
                .FirstOrDefault(it => it.Id.Equals(request.MerchType)));
            newMerchandiseRequest.AddMerchPack(merchPack);


            var createResult =
                await _merchandiseRequestRepository.CreateAsync(newMerchandiseRequest, cancellationToken);
            await _merchandiseRequestRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}