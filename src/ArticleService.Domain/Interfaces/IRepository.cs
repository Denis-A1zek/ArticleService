using System.Linq.Expressions;

namespace ArticleService.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetQueryable();

    Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        bool tracked = true,
        params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null, 
        bool tracked = true, 
        params Expression<Func<T, object>>[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    Task CreateAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}