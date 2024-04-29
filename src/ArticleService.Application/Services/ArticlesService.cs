using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Interfaces;
using CSharpFunctionalExtensions;

namespace ArticleService.Application.Services;

public class ArticlesService : IArticlesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Article> _articleRepository;

    public ArticlesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = _unitOfWork.GetRepository<Article>();
    }
    
    public async Task<Result> CreateAsync(Guid userId, EditableArticle article)
    {
        var articleEntity = Article.Create(
            article.Tite,
            article.Annotation,
            article.Description,
            article.ImageUrl,
            userId);

        await _articleRepository.CreateAsync(articleEntity.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid articleId)
    {
        var article = await _articleRepository
            .GetFirstOrDefaultAsync(a => a.Id == articleId);

        if (article is null) return Result.Failure("Article not found");

        _articleRepository.Remove(article);
        await _unitOfWork.SaveAsync();
        return Result.Success();
    }

    public Task<Result> UpdateAsync(EditableArticle article)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateViewsAsync(Guid articleId)
    {
        throw new NotImplementedException();
    }
}