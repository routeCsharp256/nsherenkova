using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;


namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchandiseDomainService
    {

        public static MerchandiseRequest CreateMerchandiseRequest(
            Employee employee, IEnumerable<Manager> managers, MerchPack merchPack)
        {
            var request = new MerchandiseRequest();
            request.Create(employee.Id, employee.PhoneNumber, merchPack);
            if (!managers.Any(m => m.CanHandleNewTask))
            {
                throw new Exception("No vacant managers");
            }

            var responsibleManager = managers.OrderBy(m => m.AssignedTasks).First();

            request.AssignTo(responsibleManager.Id);
            responsibleManager.AssignTask();

            return request;
        }
    }
}