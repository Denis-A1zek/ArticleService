using ArticleService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
    
    public Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction == null)
        {
            return _context.Database.BeginTransactionAsync();
        }

        return useIfExists ? Task.FromResult(transaction) : _context.Database.BeginTransactionAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}