using MakeupStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeupStore.DAL.Context
{
    public class MakeupStoreDbContext : DbContext
    {
        public MakeupStoreDbContext(DbContextOptions<MakeupStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}
