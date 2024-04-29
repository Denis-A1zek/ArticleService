using CSharpFunctionalExtensions;

namespace ArticleService.Application.Interfaces.Services;

public interface IArticleSelectionService
{
    Task<Result> GetDailyAsync();
    Task<Result> GetPagedAsync();
    Task<Result> GetByFilterAsync();
}