using CoffeeVendingMachineAPI.Data;
using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachineAPI.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly CoffeeContext _context;

        public CoffeeRepository(CoffeeContext context)
        {
            _context = context;
        }

        public async Task<List<CoffeeType>> GetAllCoffeeTypesAsync()
        {
            return await _context.CoffeeTypes.ToListAsync();
        }

        public async Task<List<CoffeeCustomization>> GetAllCustomizationsAsync()
        {
            return await _context.Customizations.ToListAsync();
        }

        public async Task<CoffeeType?> GetCoffeeTypeByIdAsync(int id)
        {
            return await _context.CoffeeTypes.FindAsync(id);
        }

        public async Task<List<CoffeeCustomization>> GetCustomizationsByIdsAsync(List<int> ids)
        {
            return await _context.Customizations.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<CoffeeOrder> AddOrderAsync(CoffeeOrder order)
        {
            _context.CoffeeOrders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
