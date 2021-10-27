using System;

namespace OzonEdu.MerchandiseService.Models
{
    public class MerchItem
    {
        public MerchItem( string itemName, long employeeId)
        {
            ItemId = new Guid();
            ItemName = itemName;
            EmployeeId = employeeId;
        }
        public Guid ItemId { get; }
        public string ItemName { get; }
        public long EmployeeId { get; }
    }
}