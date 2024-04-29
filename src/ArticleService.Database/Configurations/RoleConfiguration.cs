using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UuidExtensions;

namespace ArticleService.Database.Configurations;

public class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("name");

        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = Uuid7.Guid(),
                Name = "User"
            },
            new Role()
            {
                Id = Uuid7.Guid(),
                Name = "Admin"
            },
            new Role()
            {
                Id = Uuid7.Guid(),
                Name = "Moderator"
            }
        });
        
    }
}
