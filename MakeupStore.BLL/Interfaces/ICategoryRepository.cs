using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<ProductCategory>
    {
        ProductCategory GetByName(string? name);
    }
}
