using CoffeeVendingMachineAPI.Services;
using CoffeeVendingMachineAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVendingMachineAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetCoffeeTypes()
        {
            var types = await _coffeeService.GetCoffeeTypesAsync();
            return Ok(types);
        }

        [HttpGet("customizations")]
        public async Task<IActionResult> GetCustomizations()
        {
            var customizations = await _coffeeService.GetCustomizationsAsync();
            return Ok(customizations);
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var order = await _coffeeService.CreateOrderAsync(
                request.CoffeeTypeId,
                request.ExternalCoffeeName,
                request.CustomizationIds
            );

            return Ok(order);
        }

        public class OrderRequest
        {
            public int? CoffeeTypeId { get; set; }
            public string? ExternalCoffeeName { get; set; }
            public List<int> CustomizationIds { get; set; } = new();
        }
    }

}
