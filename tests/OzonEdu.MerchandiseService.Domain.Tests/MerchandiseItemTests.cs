using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchandiseItemTests
    {
        [Fact]
        public void CreateMerchandiseItem()
        {
            //Arrange 
            var merchPack = "StarterPack";

            //Act
            var merchaItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
        
            //Assert
            Assert.Equal(merchPack, merchaItem.MerchPack.MerchType.Name);
        }
        
        [Fact]
        public void MerchandiseItemAddItems ()
        {
            //Arrange 
            var items = new List<Sku>{new Sku(1), new Sku(2)};

            //Act
            var merchItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            merchItem.AddSku(new List<Sku>{new Sku(1), new Sku(2)});
        
            //Assert
            Assert.Equal(items, merchItem.Items);
        }
        [Fact]
        public void MerchandiseItemAddNullItems ()
        {
            //Arrange 

            //Act
            var merchItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
           
            void action() =>  merchItem.AddSku(null);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }
        [Fact]
        public void MerchandiseFullItemAddItems()
        {
            //Arrange 

            //Act
            var merchItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack),
                new List<Sku>{new Sku(1), new Sku(2)});
           
            void action() =>  merchItem.AddSku(new List<Sku>{new Sku(1), new Sku(2)});

            //Assert
            Assert.Throws<Exception>(action);
        }
    }
}