using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Repositories.Base;

namespace StoreModuleClient.Models.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public virtual T GetById(int id) => _context.Set<T>().Find(id);

    public virtual async void Add(T enrity)
    {
        _context.Set<T>().Add(enrity);
        await _context.SaveChangesAsync();
    }

    public virtual async void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    
}