using System;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class MerchItemResponse
    {
        public long ItemId { get; set; }
        
        public string ItemName { get; set; }
        
        public long EmployeeId { get; set;}
    }
}