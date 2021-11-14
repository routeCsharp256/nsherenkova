using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchandiseRequestTests
    {
        [Fact]
        public void AddMerchPack_CorrectArgumentAndStatus_Equal()
        {
            //Arrange 
            var employeeId = 1;
            var phoneNumber = new PhoneNumber("88888888888"); 
            var merchandiseItem =  new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            var status = MerchandiseRequestStatus.Created;
        
            //Act
            var merchandiseRequest = new MerchandiseRequest(employeeId, phoneNumber);
            merchandiseRequest.AddMerchPack(new MerchPack(MerchType.StarterPack));
        
            //Assert
            Assert.Equal(employeeId, merchandiseRequest.EmployeeId);
            Assert.Equal(phoneNumber, merchandiseRequest.ContactPhone);
            Assert.Equal(merchandiseItem.MerchPack.MerchType.Name,
                merchandiseRequest.MerchandiseItem.MerchPack.MerchType.Name);
            Assert.Equal(status, merchandiseRequest.Status);
        }

        [Fact]
        public void AddMerchPack_WhenMerchandiseRequestStatusCreated_Throws()
        {
            //Arrange 

            var phoneNumber = new PhoneNumber("88888888888");
            var merchandiseItem = new MerchandiseItem(new MerchPack(MerchType.StarterPack));

            //Act
            var merchandiseRequest = new MerchandiseRequest(1, phoneNumber, merchandiseItem);
            void action() => merchandiseRequest.AddMerchPack(new MerchPack(MerchType.StarterPack));

            //Assert
            Assert.Throws<Exception>(action);
        }
        [Fact]
        public void AssignTo_WhenIdManagerIsNegative_Throws()
        {
            //Arrange 

            var managerId = -10;

            //Act
            var merchandiseRequest = new MerchandiseRequest(1, new PhoneNumber("88888888888"),
                new MerchandiseItem(new MerchPack(MerchType.StarterPack)));
            void action() => merchandiseRequest.AssignTo(managerId);

            //Assert
            Assert.Throws<NegativeValueException>(action);
        }
        [Fact]
        public void AssignTo_WhenIdManagerAndStatusIsCorrect_Equal()
        {
            //Arrange 
            var employeeId = 1;
            var managerId = 1;
            var phoneNumber = new PhoneNumber("88888888888"); 
            var merchandiseItem =  new MerchandiseItem(new MerchPack(MerchType.StarterPack));
            var status = MerchandiseRequestStatus.Assigned;
        
            //Act
            var merchandiseRequest = new MerchandiseRequest(employeeId, phoneNumber, merchandiseItem);
            merchandiseRequest.AssignTo(managerId);
        
            //Assert
            Assert.Equal(employeeId, merchandiseRequest.EmployeeId);
            Assert.Equal(phoneNumber, merchandiseRequest.ContactPhone);
            Assert.Equal(merchandiseItem.MerchPack.MerchType.Name,
                merchandiseRequest.MerchandiseItem.MerchPack.MerchType.Name);
            Assert.Equal(managerId, merchandiseRequest.ResponsibleManagerId);
            Assert.Equal(status, merchandiseRequest.Status);
        }
    }
}