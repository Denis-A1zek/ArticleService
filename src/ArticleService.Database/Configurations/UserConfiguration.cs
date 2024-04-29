using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UuidExtensions;

namespace ArticleService.Database.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnName("login");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasColumnName("password");
        
        builder.HasOne(u => u.Token)
            .WithOne(r => r.User)
            .HasForeignKey<UserToken>(r => r.UserId);

        builder.Property(u => u.UserTokenId)
            .HasColumnName("user_token_id");
        
        builder.Property(a => a.CreatedAt) 
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
            .HasColumnName("upated_at");
        
        builder.HasMany<Article>(u => u.Articles)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .HasPrincipalKey(u => u.Id);

        builder.Property(u => u.RoleId).HasColumnName("role_id");
        builder.HasOne(u => u.Role) 
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}