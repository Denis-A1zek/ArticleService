using ArticleService.Application.Dtos;
using ArticleService.Domain.Interfaces;
using CSharpFunctionalExtensions;

namespace ArticleService.Application.Interfaces.Services;

public interface IArticlesService
{
    Task<Result> CreateAsync(Guid userId, EditableArticle article);
    Task<Result> DeleteAsync(Guid articleId);
    Task<Result> UpdateAsync(EditableArticle article);
    Task<Result> UpdateViewsAsync(Guid articleId);
}