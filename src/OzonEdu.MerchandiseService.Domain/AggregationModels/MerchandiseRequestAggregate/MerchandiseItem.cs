using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseItem : Entity
    {
        public MerchandiseItem(MerchPack merchPack)
        {
            MerchPack = merchPack;
        }

        public MerchandiseItem(MerchPack merchPack, List<Sku> items) : this(merchPack)
        {
            Items = items;
        }

        public MerchPack MerchPack { get;  }
        public List<Sku> Items { get; set; }
        
        /// <summary>
        /// Дабавляем конкретные Sku в пакет
        /// </summary>
        public void AddSku(List<Sku> items)
        {
            if (Items is not null)
                throw new Exception("The list of Sku is already filled in");
            Items = items?? throw new ArgumentException("The list of Sku should not be null");
        }
    }
}