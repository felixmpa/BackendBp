using Moq;
using Xunit;
using Bp.Client.Service.Controllers;
using Bp.Client.Service.Entities;
using Bp.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Bp.Client.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<IRepository<Customer>> _mockRepo;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockRepo = new Mock<IRepository<Customer>>();
            _controller = new CustomersController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_WhenIdNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Customer, object>>>()))
                     .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetByIdAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCustomer_WhenIdIsFound()
        {
            // Arrange
            var expectedCustomer = new Customer { Id = 1 };
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Customer, object>>>()))
                     .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _controller.GetByIdAsync(1);

            // Assert
            Assert.IsType<Customer>(result.Value);
            Assert.Equal(expectedCustomer.Id, result.Value.Id);
        }


        [Fact]
        public async Task PutAsync_WithValidIdAndCustomer_ReturnsOkResult()
        {
            // Arrange
            int testId = 1;
            var testIdentification = "0012345678";
            var testName = "Felix Perez";
            var testSex = Sex.Male;
            var testAge = 32;
            var testAddress = "St Test #123";
            var testPhone = "0001235555";
            var testPassword = "1234";
            var testStatus = CustomerStatus.Inactive;

            var existingCustomer = new Customer
            {
                Id = testId,
                Status = testStatus,
                Password = testPassword,
                Person = new Person
                {
                    Identification = testIdentification,
                    Name = testName,
                    Sex = testSex,
                    Age = testAge,
                    Address = testAddress,
                    Phone = testPhone
                }
            };
            var updatedCustomer = new Customer
            {
                Id = testId,
                Status = CustomerStatus.Active,
                Password = "newPwd123!",
                Person = new Person
                {
                    Identification = testIdentification,
                    Name = testName,
                    Sex = testSex,
                    Age = testAge,
                    Address = "St Test #123 Dominican Republic",
                    Phone = testPhone
                }
            };

            _mockRepo.Setup(repo => repo.GetAsync(testId))
                     .ReturnsAsync(existingCustomer);
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Customer>()))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAsync(testId, updatedCustomer);
            var assertCustomerResult = await _controller.GetByIdAsync(testId);
            var assertCustomer = assertCustomerResult.Value;

            // Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.NotNull(assertCustomerResult);
            Assert.IsType<ActionResult<Customer>>(assertCustomerResult);
            //Assert.NotNull(assertCustomer);
            //Assert.NotNull(assertCustomer); 
            //Assert.Equal(updatedCustomer.Password, assertCustomer.Password);
        }

    }
}
