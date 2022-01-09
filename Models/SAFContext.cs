using Microsoft.EntityFrameworkCore;

namespace SpiritualityAndFate.Models
{
    public class SAFContext : DbContext
    {
        public SAFContext(DbContextOptions options) : base(options) {}
        public DbSet<Player> Players {get; set;}
        public DbSet<Item> Items {get; set;}
    }
}