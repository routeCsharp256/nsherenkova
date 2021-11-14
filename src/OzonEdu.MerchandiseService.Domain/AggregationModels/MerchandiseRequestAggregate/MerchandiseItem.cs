using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseItem : Entity
    {
        public MerchPack MerchPack { get; }
        public List<Sku> Items { get; }

        public MerchandiseItem(MerchPack merchPack)
        {
            if (merchPack is null)
                throw new ArgumentException("The type of merch must be determined");
            MerchPack = merchPack;
            Items = new List<Sku>();
        }

        public MerchandiseItem(MerchPack merchPack, List<Sku> items) : this(merchPack)
        {
            if (items is null)
                throw new ArgumentException("The list of Sku should not be null");
            AddRange(items);
        }


        /// <summary>
        /// Дабавляем конкретные Sku в пакет
        /// </summary>
        public void AddRange(List<Sku> items)
        {
            if (items is null)
                throw new ArgumentException("The list of Sku should not be null");
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}