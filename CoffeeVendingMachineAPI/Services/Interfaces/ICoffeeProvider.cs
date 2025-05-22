using CoffeeVendingMachineAPI.Models;

namespace CoffeeVendingMachineAPI.Services.Interfaces
{
    public interface ICoffeeProvider
    {
        Task<IEnumerable<CoffeeType>> GetExternalCoffeeTypesAsync();
    }
}
