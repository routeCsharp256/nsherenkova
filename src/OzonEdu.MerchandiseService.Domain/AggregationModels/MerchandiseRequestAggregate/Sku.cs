using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class Sku : ValueObject
    {
        public long Value { get; }
        
        public Sku(long sku)
        {
            if (sku < 0)
            {
                throw new NegativeValueException($"{nameof(sku)} value is negative");
            }
            Value = sku;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}