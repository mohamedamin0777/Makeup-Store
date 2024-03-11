using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<ProductBrand> GetProductsByBrand(int? id);
        IEnumerable<ProductCategory> GetProductsByCategory(int? id);
        public IEnumerable<Product> Search(string name);

    }
}
