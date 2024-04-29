using Microsoft.EntityFrameworkCore.Storage;

namespace ArticleService.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false);
}