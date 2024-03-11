using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Context;
using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<ProductCategory>, ICategoryRepository
    {
        private readonly MakeupStoreDbContext _context;

        public CategoryRepository(MakeupStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public ProductCategory GetByName(string? name)
            => _context.ProductCategories.Where(cat => cat.Name == name).FirstOrDefault();
    }
}
