using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Context;
using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MakeupStoreDbContext _context;
        public ProductRepository(MakeupStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ProductBrand> GetProductsByBrand(int? id)
            => (IEnumerable<ProductBrand>)_context.Products.Where(p => p.ProductBrandId == id);

        public IEnumerable<ProductCategory> GetProductsByCategory(int? id)
            => (IEnumerable<ProductCategory>)_context.Products.Where(p => p.ProductCategoryId == id);

        public IEnumerable<Product> Search(string name)
            => _context.Products.Where(p => p.Name.Trim().ToLower().Contains(name.Trim().ToLower()));

    }
}
