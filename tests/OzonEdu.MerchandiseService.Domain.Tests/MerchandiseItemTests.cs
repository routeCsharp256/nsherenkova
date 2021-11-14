using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchandiseItemTests
    {
        [Fact]
        public void CreateMerchandiseItem_WhenMerchTypeNameStarterPack_CreateMerchandiseItemMerchPackMerchTypeNameStarterPack()
        {
            //Arrange 
            var merchPack = "StarterPack";

            //Act
            var merchaItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
        
            //Assert
            Assert.Equal(merchPack, merchaItem.MerchPack.MerchType.Name);
        }
        [Fact]
        public void CreateMerchandiseItem_WhenMerchPackIsNull_Throws()
        {
            //Arrange 

            //Act

            void action() =>  new MerchandiseItem(null);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }
        [Fact]
        public void AddRange_WhenListSkuIsNotNull_MerchItemAddedListSku ()
        {
            //Arrange 
            var items = new List<Sku>{new Sku(1), new Sku(2)};

            //Act
            var merchItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            merchItem.AddRange(new List<Sku>{new Sku(1), new Sku(2)});
        
            //Assert
            Assert.Equal(items, merchItem.Items);
        }
        [Fact]
        public void AddRange_WhenListSkuIsNull_Throws()
        {
            //Arrange 

            //Act
            var merchItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
           
            void action() =>  merchItem.AddRange(null);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}