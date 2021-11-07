using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchandiseRequestTests
    {
        [Fact]
        public void CreateMerchandiseRequestExistEmployee()
        {
            //Arrange 

            var phoneNumber = new PhoneNumber("88888888888"); 
            var merchandiseItem =  new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            var status = MerchandiseRequestStatus.Created;
        
            //Act
            var merchandiseRequest = new MerchandiseRequest(1, new PhoneNumber("88888888888"));
            merchandiseRequest.Create(new MerchPack(MerchType.StarterPack));
        
            //Assert
            Assert.Equal(1, merchandiseRequest.EmployeeId);
            Assert.Equal(phoneNumber, merchandiseRequest.ContactPhone);
            Assert.Equal(merchandiseItem.MerchPack.MerchType.Name,
                merchandiseRequest.MerchandiseItem.MerchPack.MerchType.Name);
            Assert.Equal(status, merchandiseRequest.Status);
        }

        [Fact]
        public void CreateMerchandiseRequestStatusCreated()
        {
            //Arrange 

            var phoneNumber = new PhoneNumber("88888888888");
            var merchandiseItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            var status = MerchandiseRequestStatus.Created;

            //Act
            var merchandiseRequest = new MerchandiseRequest(1, new PhoneNumber("88888888888"),
                new MerchandiseItem(new MerchPack(MerchType.StarterPack)));
            void action() => merchandiseRequest.Create(new MerchPack(MerchType.StarterPack));

            //Assert
            Assert.Throws<Exception>(action);
        }
    }
}