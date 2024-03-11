using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Interfaces
{
    public interface IBrandRepository : IGenericRepository<ProductBrand>
    {
        ProductBrand GetByName(string? name);
    }
}
