using ArticleService.Domain.Interfaces;

namespace ArticleService.Domain.Entities;

public class Role : IIdentity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
}