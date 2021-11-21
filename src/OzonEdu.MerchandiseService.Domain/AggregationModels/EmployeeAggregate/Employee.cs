using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee: Entity
    {
        public Employee(PersonName name, Email email, ClothingSize size, PhoneNumber phoneNumber)
        {
            Name = name;
            Email = email;
            Size = size;
            PhoneNumber = phoneNumber;
        }

        public PersonName Name { get; }

        public Email Email { get; }

        public ClothingSize Size { get; }

        public PhoneNumber PhoneNumber { get; }
    }
}