using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get;  }
        public string LastName { get;  }

        private PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}