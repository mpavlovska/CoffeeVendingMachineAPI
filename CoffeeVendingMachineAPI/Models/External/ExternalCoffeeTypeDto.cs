using System.Text.Json.Serialization;

namespace CoffeeVendingMachineAPI.Models.External
{
    public class ExternalCoffeeTypeDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }
}
