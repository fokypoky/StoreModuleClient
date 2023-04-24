namespace StoreModuleClient.Repositories.Base;

public interface IRepository<T>
{
    public T GetById(int id);
    public void Add(T enrity);
    public void Remove(T entity);
}