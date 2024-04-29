using ArticleService.Domain.Interfaces;
using ArticleService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using UuidExtensions;

namespace ArticleService.Domain.Entities;

public record Article : IIdentity, IAuditable
{
    public const int MAX_TEXT_LENGTH = 5000;
    
    private Article(Guid id, string title, string annotation, string description,string imageUrl, Guid userId)
    {
        Id = id;
        Title = title;
        Annotation = annotation;
        Description = description;
        ImageUrl = imageUrl;
        Views = 0;
        Checked = false;
        UserId = userId;
        PublicationTime = PublicationTime.Create(DateTime.UtcNow, DateTime.UtcNow).Value;
    }
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Annotation { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string? ImageUrl { get; init; }
    public int Views { get; private set; }
    public bool Checked { get; private set; }
    public Guid UserId { get; init; }
    public User? User { get; set; }
    public PublicationTime PublicationTime { get; set; }
    public ArticleLog? Log { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public void Reviewed(DateTime start, DateTime end)
    {
        Checked = true;
        PublicationTime = PublicationTime.Create(start, end).Value;
    }

    public void CountView() => Views++;

    public static Result<Article> Create
        (string title, string annotation, string description, string imageUrl, Guid userId)
    {
        return new Article(Uuid7.Guid(), title, annotation, description,imageUrl, userId);
    }
}