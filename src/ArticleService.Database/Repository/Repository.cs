using System.Linq.Expressions;
using ArticleService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Database.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private AppDbContext _context;
    internal DbSet<T> Set;

    public Repository(AppDbContext context)
    {
        _context = context;
        Set = _context.Set<T>();
    }
    
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = Set;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync(); 
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = Set;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
        => await Set.AddAsync(entity);

    public void Update(T entity)
        => Set.Update(entity);

    public void Remove(T entity)
        => Set.Remove(entity);
}