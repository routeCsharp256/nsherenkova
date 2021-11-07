using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }
        
        public PhoneNumber(string value)
        {
            if (IsPhoneNumber(value)) 
                Value = value;
            else
            {
                throw new AggregateException($"Phone Number {value} incorrectly");
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        private static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0-9]{11})$").Success;
        }
    }
}