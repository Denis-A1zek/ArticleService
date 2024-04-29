using ArticleService.Application.Interfaces.Services;
using ArticleService.Application.Interfaces.Services.Auth;
using ArticleService.Application.Services;
using ArticleService.Application.Services.Auth;
using ArticleService.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Application;

public static class Dependencies
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<IArticleReviewService, ArticleReviewService>();
        services.AddScoped<IArticlesService, ArticlesService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IKeyGenerator, KeyGenerator>();
    }
}