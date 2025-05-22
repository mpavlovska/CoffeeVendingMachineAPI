using CoffeeVendingMachineAPI.Models;

namespace CoffeeVendingMachineAPI.Repositories.Interfaces
{
    public interface ICoffeeRepository
    {
        Task<List<CoffeeType>> GetAllCoffeeTypesAsync();
        Task<List<CoffeeCustomization>> GetAllCustomizationsAsync();
        Task<CoffeeType?> GetCoffeeTypeByIdAsync(int id);
        Task<List<CoffeeCustomization>> GetCustomizationsByIdsAsync(List<int> ids);
        Task<CoffeeOrder> AddOrderAsync(CoffeeOrder order);
    }
}
