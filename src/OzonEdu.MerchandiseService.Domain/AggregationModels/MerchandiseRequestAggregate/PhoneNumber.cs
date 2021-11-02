using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string value)
            => Value = value;

        public string Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}