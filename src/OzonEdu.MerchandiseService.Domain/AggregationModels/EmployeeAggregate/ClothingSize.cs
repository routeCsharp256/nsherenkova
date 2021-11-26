using System;
using OzonEdu.MerchandiseService.Domain.Models;


namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new(1, nameof(XS));
        public static ClothingSize S = new(2, nameof(S));
        public static ClothingSize M = new(3, nameof(M));
        public static ClothingSize L = new(4, nameof(L));
        public static ClothingSize XL = new(5, nameof(XL));
        public static ClothingSize XXL = new(6, nameof(XXL));

        public ClothingSize(int id, string name) : base(id, name)
        {
        }

        public static ClothingSize Parse(int a)
        {
            switch (a)
            {
                case 1: return XS;
                case 2: return S;
                case 3: return M;
                case 4: return L;
                case 5: return XL;
                case 6: return XXL;
            }
            throw new ArgumentException("There is no such size");
        }
    }
}