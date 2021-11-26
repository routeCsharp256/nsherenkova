using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Models
{
    public class EmployeeCreationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Email { get; set;}
        public string PhoneNumber { get; set;}
        public int Size { get; set;}
    }
}