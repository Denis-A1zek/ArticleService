using ArticleService.Domain.Interfaces;
using CSharpFunctionalExtensions;

namespace ArticleService.Domain.Entities;

public record Article : IIdentity, IAuditable
{
    public const int MAX_TEXT_LENGTH = 5000;
    
    private Article(Guid id, string title, string annotation, string imageUrl)
    {
        Id = id;
        Title = title;
        Annotation = annotation;
        ImageUrl = imageUrl;
        Views = 0;
    }
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Annotation { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string? ImageUrl { get; init; }
    public int Views { get; private set; }
    public ArticlesPending? ArticlesPending { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; init; }
    public Guid UpdatedBy { get; set; }

    public void CountView() => Views++;

    public static Result<Article> Create
        (Guid id, string title, string annotation, string imageUrl)
    {
        return new Article(id, title, annotation, imageUrl);
    }
}