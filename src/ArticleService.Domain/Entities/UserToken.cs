using ArticleService.Domain.Interfaces;

namespace ArticleService.Domain.Entities;

public class UserToken : IIdentity
{
    public Guid Id { get; init; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}