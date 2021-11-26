using System;
using System.Dynamic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        private Employee(PersonName name, Email email,  PhoneNumber phoneNumber, ClothingSize size)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Size = size;
        }
        public PersonName Name { get; }
        public Email Email { get; }
        public PhoneNumber PhoneNumber { get; }
        public ClothingSize Size { get; }


        public static Employee Create(PersonName name, Email email, PhoneNumber phoneNumber, ClothingSize size)
        {
            if (name == null || email == null || phoneNumber == null || size == null)
                throw new ArgumentNullException("All arguments cannot be null");
            var newEmployee = new Employee(name, email, phoneNumber, size);
            return newEmployee;
        }
    }
}