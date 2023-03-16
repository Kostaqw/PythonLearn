namespace PythonLearn.DAL;

public interface IBaseRepository<T>
{
    public Task<T?> GetAsync(int id);
    IQueryable<T> GetAllAsync();
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(int id);
}

