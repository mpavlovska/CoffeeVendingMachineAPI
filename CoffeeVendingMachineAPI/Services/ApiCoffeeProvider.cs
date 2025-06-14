﻿using CoffeeVendingMachineAPI.Models;
using CoffeeVendingMachineAPI.Models.External;
using CoffeeVendingMachineAPI.Services.Interfaces;
using System;
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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", "2025-api-key");
            var externalDtos = await client.GetFromJsonAsync<List<ExternalCoffeeTypeDto>>("https://coffeedataproviderapi-production.up.railway.app/api/external-coffee-types");

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
