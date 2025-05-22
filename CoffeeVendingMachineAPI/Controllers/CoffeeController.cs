using Microsoft.AspNetCore.Mvc;
using CoffeeVendingMachineAPI.Services.Interfaces;

namespace CoffeeVendingMachineAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _service;

        public CoffeeController(ICoffeeService service)
        {
            _service = service;
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetCoffeeTypes()
        {
            var types = await _service.GetCoffeeTypesAsync();
            return Ok(types);
        }

        [HttpGet("customizations")]
        public async Task<IActionResult> GetCustomizations()
        {
            var customizations = await _service.GetCustomizationsAsync();
            return Ok(customizations);
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var order = await _service.CreateOrderAsync(request.CoffeeTypeId, request.CustomizationIds);
            return Ok(order);
        }

        public class OrderRequest
        {
            public int CoffeeTypeId { get; set; }
            public List<int> CustomizationIds { get; set; } = new();
        }
    }

}
