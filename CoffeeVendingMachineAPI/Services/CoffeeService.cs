using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Repositories.Interfaces;
using CoffeeVendingMachineAPI.Services.Interfaces;

namespace CoffeeVendingMachineAPI.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly ICoffeeRepository _repository;
        private readonly ICoffeeProvider _externalProvider;

        public CoffeeService(ICoffeeRepository repository, ICoffeeProvider externalProvider)
        {
            _repository = repository;
            _externalProvider = externalProvider;
        }

        public async Task<IEnumerable<CoffeeType>> GetCoffeeTypesAsync()
        {
            var local = await _repository.GetAllCoffeeTypesAsync();
            var external = await _externalProvider.GetExternalCoffeeTypesAsync();

            return local.Concat(external).GroupBy(c => c.Name).Select(g => g.First());
        }

        public async Task<IEnumerable<CoffeeCustomization>> GetCustomizationsAsync()
        {
            return await _repository.GetAllCustomizationsAsync();
        }

        public async Task<CoffeeOrder> CreateOrderAsync(int? coffeeTypeId, string? externalCoffeeName, List<int> customizationIds)
        {
            if (coffeeTypeId == null && string.IsNullOrWhiteSpace(externalCoffeeName))
                throw new Exception("An internal or external coffee type is required.");

            CoffeeType? coffeeType = null;

            if (string.IsNullOrWhiteSpace(externalCoffeeName))
            {
                coffeeType = await _repository.GetCoffeeTypeByIdAsync(coffeeTypeId.Value);
                if (coffeeType == null)
                    throw new Exception("Internal coffee type not found.");
            }

            var customizations = await _repository.GetCustomizationsByIdsAsync(customizationIds);

            var order = new CoffeeOrder
            {
                CoffeeType = coffeeType,
                ExternalCoffeeName = coffeeType == null ? externalCoffeeName : null,
                Customizations = customizations
            };

            return await _repository.AddOrderAsync(order);
        }

    }
}
