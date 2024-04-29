namespace ArticleService.Application.Interfaces.Services.Auth;

public interface IPasswordHasher
{
    public string Hash(string password);
    public bool Verify(string passwordHash, string passwordInput);
}