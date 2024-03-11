using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Context;
using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Repositories
{
    public class BrandRepository : GenericRepository<ProductBrand>, IBrandRepository
    {
        private readonly MakeupStoreDbContext _context;

        public BrandRepository(MakeupStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public ProductBrand GetByName(string? name)
            => _context.ProductBrands.Where(b => b.Name == name).FirstOrDefault();
    }
}
