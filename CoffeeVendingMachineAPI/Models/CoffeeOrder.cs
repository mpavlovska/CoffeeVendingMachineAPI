namespace CoffeeVendingMachineAPI.Models
{
    public class CoffeeOrder
    {
        public int Id { get; set; }

        public int? CoffeeTypeId { get; set; }

        public CoffeeType? CoffeeType { get; set; }

        public string? ExternalCoffeeName { get; set; }

        public List<CoffeeCustomization> Customizations { get; set; } = new();
    }
}
