using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Models.External;
using CoffeeVendingMachineAPI.Services.Interfaces;
using System.Net.Http.Json;

namespace CoffeeVendingMachineAPI.Services
{
    public class ApiCoffeeProvider : ICoffeeProvider
    {
        private readonly HttpClient _httpClient;

        public ApiCoffeeProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CoffeeType>> GetExternalCoffeeTypesAsync()
        {
            var externalDtos = await _httpClient.GetFromJsonAsync<List<ExternalCoffeeTypeDto>>("https://api.sampleapis.com/coffee/hot");
            if (externalDtos == null) return new List<CoffeeType>();

            return externalDtos.Select(dto => new CoffeeType
            {
                Id = dto.Id + 1000,
                Name = dto.Title,
                Description = dto.Description
            });
        }
    }
}
