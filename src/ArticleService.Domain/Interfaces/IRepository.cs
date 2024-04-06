using System.Linq.Expressions;

namespace ArticleService.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    Task CreateAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}