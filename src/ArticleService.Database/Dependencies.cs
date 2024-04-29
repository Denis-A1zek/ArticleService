using ArticleService.Database.Repository;
using ArticleService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Database;

public static class Dependencies
{
    public static void AddInfrastruture(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(o =>
        {
            o.UseNpgsql("Host=localhost; Port=5432; Database=articles; Username=dsigida; Password=V4t_nSA9D;");
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}