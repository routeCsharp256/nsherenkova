using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseRequest: Entity
    {
        /// <summary>
        /// Текущий статус запроса
        /// </summary>
        public MerchandiseRequestStatus Status { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника, которому предназначен мерч
        /// </summary>
        public long EmployeeId { get; private set; }

        /// <summary>
        /// Телефон для связи с сотрудником
        /// </summary>
        public PhoneNumber ContactPhone { get; private set; }

        /// <summary>
        /// ID ответственного менеджера за выдачу мерча
        /// </summary>
        public long ResponsibleManagerId { get; private set; }

        /// <summary>
        /// Коллекция мерча
        /// </summary>
        public MerchandiseItem MerchandiseItem { get; private set; }

        public MerchandiseRequest(long employeeId, PhoneNumber contactPhone)
        {
            EmployeeId = employeeId;
            ContactPhone = contactPhone ?? throw new Exception("Phone should not be null");
            Status = MerchandiseRequestStatus.Draft;
        }

        public MerchandiseRequest(long employeeId, PhoneNumber contactPhone, MerchandiseItem items)
            : this(employeeId, contactPhone)
        {
            MerchandiseItem = items;
            Status = MerchandiseRequestStatus.Created;
        }

        public MerchandiseRequest(long employeeId, PhoneNumber contactPhone,  
            MerchandiseItem items, long responsibleManagerId)
            : this(employeeId, contactPhone, items)
        {
            Status = MerchandiseRequestStatus.Assigned;
            ResponsibleManagerId = responsibleManagerId;
            MerchandiseItem = items;
        }

        /// <summary>
        /// Создаем заявку на пакет мерча для конкретного сотрудника 
        /// </summary>
        public void Create( MerchPack merchPack)
        {
            if (Status != MerchandiseRequestStatus.Draft)
            {
                throw new Exception("Incorrect request status");
            }
            MerchandiseItem = new MerchandiseItem(merchPack);
            Status = MerchandiseRequestStatus.Created;
        }

        /// <summary>
        /// Назначаем заявку на мерч ответственному менеджеру
        /// Если заявка не в статусе Created, то выбрасываем исключение
        /// </summary>
        public void AssignTo(long responsibleManagerId)
        {
            if (Status != MerchandiseRequestStatus.Created)
            {
                throw new Exception("Incorrect request status");
            }

            Status = MerchandiseRequestStatus.Assigned;
            ResponsibleManagerId = responsibleManagerId;
        }

        /// <summary>
        /// Берем заявку на мерч в работу
        /// </summary>
        public void StartWork(List<Sku> itemsSku)
        {
            if (Status != MerchandiseRequestStatus.Assigned)
            {
                throw new Exception("Incorrect request status");
            }

            MerchandiseItem.AddSku(itemsSku);
            Status = MerchandiseRequestStatus.InProgress;
        }

        /// <summary>
        /// Завершить работу по заявке
        /// </summary>
        public void Complete()
        {
            if (Status != MerchandiseRequestStatus.InProgress)
            {
                throw new Exception("Incorrect request status");
            }

            Status = MerchandiseRequestStatus.Done;
        }
    }

}