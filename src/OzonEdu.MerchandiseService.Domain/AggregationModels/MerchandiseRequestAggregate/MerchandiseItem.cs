using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate
{
    public class MerchandiseItem : Entity
    {
        private static int uniqueId = 0;

        public MerchandiseItem(int id, MerchPack merchPack, List<Sku> items)
        {
            Id = id;
            MerchPack = merchPack;
            Items = items;
        }

        public MerchPack MerchPack { get; }
        public List<Sku> Items { get; }

        public MerchandiseItem(MerchPack merchPack)
        {
            if (merchPack is null)
                throw new ArgumentNullException("The type of merch must be determined");
            Id = uniqueId;
            MerchPack = merchPack;
            Items = new List<Sku>();
            uniqueId++;
        }

        /// <summary>
        /// Дабавляем конкретные Sku в пакет
        /// </summary>
        public void AddRange(List<Sku> items)
        {
            if (items is null)
                throw new ArgumentNullException("The list of Sku should not be null");
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}