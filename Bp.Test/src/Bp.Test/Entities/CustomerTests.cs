using Xunit;
using Bp.Client.Service.Entities;

namespace Bp.Client.Tests.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_SetProperties_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedIdentification = "0012345678";
            var expectedName = "Felix Perez";
            var expectedSex = Sex.Male;
            var expectedAge = 32;
            var expectedAddress = "St Test #123";
            var expectedPhone = "0001235555";
            var expectedPassword = "1234";
            var expectedStatus = CustomerStatus.Active;

            var person = new Person
            {
                Identification = expectedIdentification,
                Name = expectedName,
                Sex = expectedSex,
                Age = expectedAge,
                Address = expectedAddress,
                Phone = expectedPhone
            };

            // Act
            var customer = new Customer
            {
                Person = person,
                Status = expectedStatus,
                Password = expectedPassword
            };

            // Assert
            Assert.Equal(expectedIdentification, customer.Person.Identification);
            Assert.Equal(expectedName, customer.Person.Name);
            Assert.Equal(expectedSex, customer.Person.Sex);
            Assert.Equal(expectedAge, customer.Person.Age);
            Assert.Equal(expectedAddress, customer.Person.Address);
            Assert.Equal(expectedPhone, customer.Person.Phone);
            Assert.Equal(expectedPassword, customer.Password);
            Assert.Equal(expectedStatus, customer.Status);

        }
    }
}