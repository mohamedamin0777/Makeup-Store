using MakeupStore.BLL.Interfaces;
using MakeupStore.DAL.Context;
using MakeupStore.DAL.Entities;

namespace MakeupStore.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        private readonly MakeupStoreDbContext _context;

        public GenericRepository(MakeupStoreDbContext context)
        {
            _context = context;
        }

        public T GetById(int? id)
            => _context.Set<T>().FirstOrDefault(x => x.Id == id);

        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();

        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

    }
}
