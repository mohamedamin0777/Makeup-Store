using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
