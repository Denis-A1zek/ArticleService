namespace ArticleService.Application.Dtos;

public record EditableArticle
{
    public EditableArticle(
        string title, 
        string annotation, 
        string description, 
        string? imageUrl = null)
    {
        Tite = title;
        Annotation = annotation;
        Description = description;
        ImageUrl = imageUrl;
    }

    public EditableArticle()
    {
    }

    public string Tite { get; set; }
    public string Annotation { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    
}