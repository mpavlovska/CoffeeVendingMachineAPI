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
            CoffeeType? coffeeType = await _repository.GetCoffeeTypeByIdAsync(coffeeTypeId.Value);
            List<CoffeeCustomization> customizations = await _repository.GetCustomizationsByIdsAsync(customizationIds);
            CoffeeOrder coffeeOrder = null;

            if (coffeeType == null)
            {
                coffeeOrder = new CoffeeOrder
                {
                    CoffeeType = null,
                    ExternalCoffeeName = externalCoffeeName,
                    Customizations = customizations
                };
            }
            else
            {
                coffeeOrder = new CoffeeOrder
                {
                    CoffeeType = coffeeType,
                    ExternalCoffeeName = null,
                    Customizations = customizations
                };
            }

            return await _repository.AddOrderAsync(coffeeOrder);
        }

    }
}
