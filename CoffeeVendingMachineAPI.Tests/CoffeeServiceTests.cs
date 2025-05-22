using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeVendingMachineAPI.Services;
using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Repositories.Interfaces;

namespace CoffeeVendingMachineAPI.Tests.Services
{
    public class CoffeeServiceTests
    {
        private readonly Mock<ICoffeeRepository> _repoMock;
        private readonly CoffeeService _service;

        public CoffeeServiceTests()
        {
            _repoMock = new Mock<ICoffeeRepository>();
            _service = new CoffeeService(_repoMock.Object);
        }

        [Fact]
        public async Task GetCoffeeTypesAsync_ReturnsList()
        {
            // Arrange
            var mockData = new List<CoffeeType> {
                new CoffeeType { Id = 1, Name = "Latte", Description = "Milk coffee" }
            };

            _repoMock.Setup(r => r.GetAllCoffeeTypesAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _service.GetCoffeeTypesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Latte", result.First().Name);
        }

        [Fact]
        public async Task CreateOrderAsync_ValidIds_CreatesOrder()
        {
            // Arrange
            var coffeeTypeId = 1;
            var customizationIds = new List<int> { 2 };

            _repoMock.Setup(r => r.GetCoffeeTypeByIdAsync(coffeeTypeId))
                .ReturnsAsync(new CoffeeType { Id = coffeeTypeId, Name = "Espresso" });

            _repoMock.Setup(r => r.GetCustomizationsByIdsAsync(customizationIds))
                .ReturnsAsync(new List<CoffeeCustomization> {
                    new CoffeeCustomization { Id = 2, Name = "Sugar" }
                });

            _repoMock.Setup(r => r.AddOrderAsync(It.IsAny<CoffeeOrder>()))
                .ReturnsAsync((CoffeeOrder o) => o);

            // Act
            var order = await _service.CreateOrderAsync(coffeeTypeId, customizationIds);

            // Assert
            Assert.Equal("Espresso", order.CoffeeType.Name);
            Assert.Single(order.Customizations);
        }

        [Fact]
        public async Task CreateOrderAsync_InvalidCoffeeType_ThrowsException()
        {
            // Arrange
            var invalidCoffeeTypeId = 99;
            var customizationIds = new List<int> { 1 };

            _repoMock.Setup(r => r.GetCoffeeTypeByIdAsync(invalidCoffeeTypeId))
                     .ReturnsAsync((CoffeeType?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.CreateOrderAsync(invalidCoffeeTypeId, customizationIds));

            Assert.Equal("Coffee type not found", ex.Message);
        }
    }
}
