using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Events;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseRequest: Entity
    {
        public MerchandiseRequest(int id, MerchandiseRequestStatus status, long employeeId, PhoneNumber contactPhone, 
            long? responsibleManagerId, MerchandiseItem merchandiseItem)
        {
            Id = id;
            Status = status;
            EmployeeId = employeeId;
            ContactPhone = contactPhone;
            ResponsibleManagerId = responsibleManagerId;
            MerchandiseItem = merchandiseItem;
        }

        /// <summary>
        /// Текущий статус запроса
        /// </summary>
        public MerchandiseRequestStatus Status { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника, которому предназначен мерч
        /// </summary>
        public long EmployeeId { get;  }

        /// <summary>
        /// Телефон для связи с сотрудником
        /// </summary>
        public PhoneNumber ContactPhone { get;  }

        /// <summary>
        /// ID ответственного менеджера за выдачу мерча
        /// </summary>
        public long? ResponsibleManagerId { get; private set; }

        /// <summary>
        /// Коллекция мерча
        /// </summary>
        public MerchandiseItem MerchandiseItem { get; private set; }

        public MerchandiseRequest(long employeeId, PhoneNumber contactPhone)
        {
            if (employeeId < 0)
                throw new NegativeValueException($"{nameof(employeeId)} value is negative");
            EmployeeId = employeeId;
            ContactPhone = contactPhone ?? throw new ArgumentNullException("Phone should not be null");
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
            if (responsibleManagerId < 0)
            {
                throw new NegativeValueException($"{nameof(responsibleManagerId)} value is negative");
            }
            ResponsibleManagerId = responsibleManagerId;
            Status = MerchandiseRequestStatus.Assigned;
            MerchandiseItem = items;
        }

        /// <summary>
        /// Создаем заявку на пакет мерча для конкретного сотрудника
        /// Если заявка не в статусе Draft, то выбрасываем исключение
        /// </summary>
        public void AddMerchPack( MerchPack merchPack)
        {
            if (Status != MerchandiseRequestStatus.Draft)
            {
                throw new IncorrectRequestStatus("Add MerchPack can be only for MerchendiseRequest in 'Draft' status");
            }
            MerchandiseItem = new MerchandiseItem(merchPack);
            Status = MerchandiseRequestStatus.Created;
        }

        /// <summary>
        /// Назначаем заявку на мерч ответственному менеджеру, запрос передается в работу
        /// Если заявка не в статусе Created, то выбрасываем исключение
        /// Если Id менеджера отрицательное число, то выбрасываем исключение
        /// </summary>
        public void AssignTo(long responsibleManagerId)
        {
            if (Status != MerchandiseRequestStatus.Created)
            {
                throw new IncorrectRequestStatus("Manager can be Assigned only for MerchendiseRequest in 'Created' Status.");
            }

            if (responsibleManagerId < 0)
            {
                throw new NegativeValueException($"{nameof(responsibleManagerId)} value is negative");
            }

            ResponsibleManagerId = responsibleManagerId;
            Status = MerchandiseRequestStatus.Assigned;
        }

        /// <summary>
        /// Берем заявку на мерч в работу
        /// </summary>
        public void StartWork(List<Sku> itemsSku)
        {
            if (Status != MerchandiseRequestStatus.Assigned)
            {
                throw new IncorrectRequestStatus("StartWork can be only for MerchendiseRequest in 'Assigned' status");
            }

            MerchandiseItem.AddRange(itemsSku);
            Status = MerchandiseRequestStatus.InProgress;
            
            AddRequestToReceiveMerchElementsDomainEvent(itemsSku);
        }

        /// <summary>
        /// Завершить работу по заявке
        /// </summary>
        public void Complete()
        {
            if (Status != MerchandiseRequestStatus.InProgress)
            {
                throw new IncorrectRequestStatus("Complete of work can be only for MerchendiseRequest in 'InProgress' status");
            }

            Status = MerchandiseRequestStatus.Done;
        }

        private void AddRequestToReceiveMerchElementsDomainEvent(List<Sku> listSku)
        {
            var inProgressDomainEvent = new RequestToReceiveMerchElementsDomainEvent(listSku);
            this.AddDomainEvent(inProgressDomainEvent);
        }
    }

}