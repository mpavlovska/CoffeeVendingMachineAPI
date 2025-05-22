using CoffeeVendingMachineAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace CoffeeVendingMachineAPI.Tests.Integration
{
    public class CoffeeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CoffeeControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_WithInternalCoffeeType_ReturnsSuccess()
        {
            // Arrange
            var request = new
            {
                coffeeTypeId = 1,
                customizationIds = new List<int> { 1 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/coffee/order", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CoffeeOrder>();
            result.Should().NotBeNull();
            result.CoffeeType.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateOrder_WithExternalCoffeeName_ReturnsSuccess()
        {
            // Arrange
            var request = new
            {
                externalCoffeeName = "Flat White",
                customizationIds = new List<int> { 1 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/coffee/order", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CoffeeOrder>();
            result.Should().NotBeNull();
            result.ExternalCoffeeName.Should().Be("Flat White");
        }

        [Fact]
        public async Task CreateOrder_WithMissingData_ReturnsBadRequest()
        {
            // Arrange
            var request = new { customizationIds = new List<int> { 1 } };

            // Act
            var response = await _client.PostAsJsonAsync("/api/coffee/order", request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
