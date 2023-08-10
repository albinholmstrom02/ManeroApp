using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
    }
}
