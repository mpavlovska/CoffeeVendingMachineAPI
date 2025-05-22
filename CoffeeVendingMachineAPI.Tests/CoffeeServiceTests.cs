using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeVendingMachineAPI.Services;
using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Repositories.Interfaces;
using CoffeeVendingMachineAPI.Services.Interfaces;

namespace CoffeeVendingMachineAPI.Tests.Services
{
    public class CoffeeServiceTests
    {
        private readonly Mock<ICoffeeRepository> _repoMock;
        private readonly Mock<ICoffeeProvider> _providerMock;
        private readonly CoffeeService _service;

        public CoffeeServiceTests()
        {
            _repoMock = new Mock<ICoffeeRepository>();
            _providerMock = new Mock<ICoffeeProvider>();
            _service = new CoffeeService(_repoMock.Object, _providerMock.Object);
        }

        [Fact]
        public async Task GetCoffeeTypesAsync_ReturnsCombinedList()
        {
            // Arrange
            var local = new List<CoffeeType> {
                new CoffeeType { Id = 1, Name = "Latte", Description = "Milk coffee" }
            };

            var external = new List<CoffeeType> {
                new CoffeeType { Id = 1001, Name = "Turkish Coffee", Description = "Strong coffee" }
            };

            _repoMock.Setup(r => r.GetAllCoffeeTypesAsync()).ReturnsAsync(local);
            _providerMock.Setup(p => p.GetExternalCoffeeTypesAsync()).ReturnsAsync(external);

            // Act
            var result = await _service.GetCoffeeTypesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateOrderAsync_WithInternalCoffeeType_CreatesOrder()
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
            var order = await _service.CreateOrderAsync(coffeeTypeId, null, customizationIds);

            // Assert
            Assert.NotNull(order.CoffeeType);
            Assert.Equal("Espresso", order.CoffeeType.Name);
            Assert.Null(order.ExternalCoffeeName);
            Assert.Single(order.Customizations);
        }

        [Fact]
        public async Task CreateOrderAsync_WithExternalCoffeeName_CreatesOrder()
        {
            // Arrange
            var externalName = "Flat White";
            var customizationIds = new List<int> { 1 };

            _repoMock.Setup(r => r.GetCustomizationsByIdsAsync(customizationIds))
                .ReturnsAsync(new List<CoffeeCustomization> {
                    new CoffeeCustomization { Id = 1, Name = "Cinnamon" }
                });

            _repoMock.Setup(r => r.AddOrderAsync(It.IsAny<CoffeeOrder>()))
                .ReturnsAsync((CoffeeOrder o) => o);

            // Act
            var order = await _service.CreateOrderAsync(null, externalName, customizationIds);

            // Assert
            Assert.Null(order.CoffeeType);
            Assert.Equal("Flat White", order.ExternalCoffeeName);
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
                _service.CreateOrderAsync(invalidCoffeeTypeId, null, customizationIds));

            Assert.Equal("Internal coffee type not found.", ex.Message);
        }
    }
}
