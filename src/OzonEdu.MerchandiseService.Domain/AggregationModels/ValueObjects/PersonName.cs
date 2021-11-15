using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        public PersonName(string firstName, string lastName)
        {
            FirstName = firstName?? throw new ArgumentNullException("FirstName should not be null");
            LastName = lastName?? throw new ArgumentNullException("LastName should not be null");
        } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}