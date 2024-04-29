using ArticleService.Domain.Interfaces;

namespace ArticleService.Domain.Entities;

public class User : IIdentity, IAuditable
{
    public Guid Id { get; init; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid UserTokenId { get; set; }

    public List<Article> Articles { get; set; } = new();
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public UserToken? Token { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

}