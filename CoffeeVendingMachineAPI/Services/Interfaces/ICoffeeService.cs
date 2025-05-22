using CoffeeVendingMachineAPI.Models;

namespace CoffeeVendingMachineAPI.Services.Interfaces
{
    public interface ICoffeeService
    {
        Task<IEnumerable<CoffeeType>> GetCoffeeTypesAsync();
        Task<IEnumerable<CoffeeCustomization>> GetCustomizationsAsync();
        Task<CoffeeOrder> CreateOrderAsync(int coffeeTypeId, List<int> customizationIds);
    }
}
