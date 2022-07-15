using Microsoft.EntityFrameworkCore;

namespace TestVersta
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                    new Order { Id = 1, SenderCity = "Москва", SenderAdress = "Москва 1", RecipientCity = "Санкт-Петербург", AddressRecipient = "Санкт-Петербург 1", ParcelWeight = "3",Date = "" },
                    new Order { Id = 2, SenderCity = "Москва", SenderAdress = "Москва 2", RecipientCity = "Санкт-Петербург", AddressRecipient = "Санкт-Петербург 2", ParcelWeight = "2", Date = "" },
                    new Order { Id = 3, SenderCity = "Москва", SenderAdress = "Москва 3", RecipientCity = "Санкт-Петербург", AddressRecipient = "Санкт-Петербург 3", ParcelWeight = "4", Date = "" }
            );
        }
    }
}
