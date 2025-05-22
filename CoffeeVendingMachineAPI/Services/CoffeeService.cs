using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Repositories.Interfaces;
using CoffeeVendingMachineAPI.Services.Interfaces;

namespace CoffeeVendingMachineAPI.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly ICoffeeRepository _repository;
        private readonly ICoffeeProvider _externalProvider;

        public CoffeeService(ICoffeeRepository repository)
        {
            _repository = repository;
            //_externalProvider = externalProvider;
        }

        public async Task<IEnumerable<CoffeeType>> GetCoffeeTypesAsync()
        {
            var local = await _repository.GetAllCoffeeTypesAsync();

            //var external = await _externalProvider.GetExternalCoffeeTypesAsync();
            //return local.Concat(external).GroupBy(c => c.Name).Select(g => g.First());

            return local;
        }

        public async Task<IEnumerable<CoffeeCustomization>> GetCustomizationsAsync()
        {
            return await _repository.GetAllCustomizationsAsync();
        }

        public async Task<CoffeeOrder> CreateOrderAsync(int coffeeTypeId, List<int> customizationIds)
        {
            var coffeeType = await _repository.GetCoffeeTypeByIdAsync(coffeeTypeId);
            if (coffeeType == null)
                throw new Exception("Coffee type not found");

            var customizations = await _repository.GetCustomizationsByIdsAsync(customizationIds);

            var order = new CoffeeOrder
            {
                CoffeeType = coffeeType,
                Customizations = customizations
            };

            return await _repository.AddOrderAsync(order);
        }
    }
}
