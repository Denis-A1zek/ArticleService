namespace ArticleService.Domain.Interfaces;

public interface IAuditable
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; init; }
    public Guid UpdatedBy { get; set; }
}