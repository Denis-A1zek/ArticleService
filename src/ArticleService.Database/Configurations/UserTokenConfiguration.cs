using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleService.Database.Configurations;

public class UserTokenConfiguration: IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("users_token");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UserId)
            .HasColumnName("user_id");
        
        builder.Property(u => u.RefreshToken)
            .IsRequired()
            .HasColumnName("refresh_token");

        builder.Property(u => u.RefreshTokenExpiryTime)
            .IsRequired()
            .HasColumnName("refresh_token_time");

        builder
            .HasOne(ut => ut.User)
            .WithOne(u => u.Token)
            .HasForeignKey<UserToken>(f => f.UserId);
    }
}
