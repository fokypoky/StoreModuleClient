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

    public virtual void Add(T enrity)
    {
        _context.Set<T>().Add(enrity);
        _context.SaveChangesAsync();
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChangesAsync();
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChangesAsync();
    }
}