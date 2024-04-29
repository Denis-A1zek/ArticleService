using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Interfaces;
using ArticleService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Application.Services;

public class ArticleReviewService : IArticleReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Article> _artileRepository;
    private readonly IRepository<ArticleLog> _articleLogRepository;

    public ArticleReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _artileRepository = _unitOfWork.GetRepository<Article>();
        _articleLogRepository = _unitOfWork.GetRepository<ArticleLog>();
    }
    
    public async Task<Result<IEnumerable<ReviewArticle>>> GetForReviewAsync()
    {
        var articlesFromDb = await _artileRepository.GetQueryable()
            .Include(a => a.Log)
            .Where(a => a.Checked == false & a.Log.Status.Value.Equals(Status.Pending.Value))
            .ToListAsync();

        var artileForReview = articlesFromDb.Select(a => new ReviewArticle()
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            Annotation = a.Annotation,
            ImageUrl = a.ImageUrl,
            CreatedAt = a.CreatedAt
        });

        return Result.Success(artileForReview);
    }

    public async Task<Result> SubmitAsync(VerifiedArticle verifiedArticle)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var articleFromDb = await _artileRepository
                .GetAllAsync(a => a.Id == verifiedArticle.ArticleId);
            var article = articleFromDb.FirstOrDefault();
            if(article is null) return Result.Failure("Article not found");
            
            article.Reviewed(verifiedArticle.PublicationTime.Start, verifiedArticle.PublicationTime.End);
            _artileRepository.Update(article);
            await _articleLogRepository.CreateAsync(ArticleLog.Create(article.Id, Status.Approved, null));

            await _unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Failure($"Ошибка при выполнении транзакции. {ex.Message}");
        }

        return Result.Success();
    }

    public async Task<Result> RejectAsync(RejectedArticle rejectedArticle)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var articleFromDb = await _artileRepository
                .GetAllAsync(a => a.Id == rejectedArticle.ArticleId);
            var article = articleFromDb.FirstOrDefault();
            if(article is null) return Result.Failure("Article not found");
            
            await _articleLogRepository.CreateAsync(
                ArticleLog.Create(article.Id, Status.Rejected, rejectedArticle.Reason));

            await _unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Failure($"Ошибка при выполнении транзакции. {ex.Message}");
        }

        return Result.Success();
    }
}