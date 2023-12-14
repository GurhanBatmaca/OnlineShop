using Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<T>: IRepository<T>
        where T: class
    {
        protected readonly DbContext? _context;
        public EfCoreGenericRepository(DbContext? context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            _context!.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context!.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context!.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context!.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context!.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}