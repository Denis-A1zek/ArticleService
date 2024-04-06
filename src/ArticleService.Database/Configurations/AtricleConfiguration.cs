using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleService.Database.Configurations;

public class AtricleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("id");
        
        builder.Property(a => a.Title)
            .HasColumnName("title")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(a => a.Annotation)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("annotation");

        builder.Property(a => a.Description)
            .HasMaxLength(5000)
            .HasColumnName("description")
            .IsRequired();

        builder.HasOne(a => a.ArticlesPending)
            .WithOne(ap => ap.Article)
            .HasForeignKey<ArticlesPending>(ap => ap.ArticleId);

        builder.Property(a => a.ImageUrl)
            .HasColumnName("image_url");

        builder.Property(a => a.Views)
            .HasColumnName("views");

        builder.Property(a => a.CreatedAt) 
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
            .HasColumnName("upated_at");

        builder.Property(a => a.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(a => a.UpdatedBy)
            .HasColumnName("updated_by");
    }
}