using CoffeeVendingMachineAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachineAPI.Data
{
    public class CoffeeContext : DbContext
    {
        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options) { }

        public DbSet<CoffeeType> CoffeeTypes { get; set; }
        public DbSet<CoffeeCustomization> Customizations { get; set; }
        public DbSet<CoffeeOrder> CoffeeOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoffeeOrder>()
                .HasMany(c => c.Customizations)
                .WithMany()
                .UsingEntity(j => j.ToTable("OrderCustomizations"));

            modelBuilder.Entity<CoffeeType>().HasData(
                new CoffeeType { Id = 1, Name = "Espresso", Description = "Strong and black coffee" },
                new CoffeeType { Id = 2, Name = "Latte", Description = "Espresso with steamed milk" },
                new CoffeeType { Id = 3, Name = "Cappuccino", Description = "Espresso, milk, foam" },
                new CoffeeType { Id = 4, Name = "Macchiato", Description = "Espresso with a bit of milk" },
                new CoffeeType { Id = 5, Name = "Americano", Description = "Espresso with hot water" }
            );

            modelBuilder.Entity<CoffeeCustomization>().HasData(
                new CoffeeCustomization { Id = 1, Name = "Sugar" },
                new CoffeeCustomization { Id = 2, Name = "Creamer" },
                new CoffeeCustomization { Id = 3, Name = "Extra Milk" },
                new CoffeeCustomization { Id = 4, Name = "Caramel" },
                new CoffeeCustomization { Id = 5, Name = "Cinnamon" }
            );
        }
    }
}
