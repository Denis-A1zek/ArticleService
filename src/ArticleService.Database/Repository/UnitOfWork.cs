using ArticleService.Domain.Interfaces;

namespace ArticleService.Database.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        return new Repository<T>(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}