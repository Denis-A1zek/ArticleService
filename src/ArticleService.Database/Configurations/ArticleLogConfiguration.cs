using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleService.Database.Configurations;

public class ArticleLogConfiguration : IEntityTypeConfiguration<ArticleLog>
{
    public void Configure(EntityTypeBuilder<ArticleLog> builder)
    {
        builder.ToTable("article_logs");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("id");
        
        builder.Property(a => a.Reason)
            .HasColumnName("reason")
            .HasMaxLength(512);

        builder.HasOne(a => a.Article)
            .WithOne(s => s.Log)
            .HasForeignKey<ArticleLog>(a => a.ArticleId);

        builder.Property(a => a.ArticleId)
            .HasColumnName("article_id");

        builder.ComplexProperty(
            ap => ap.Status,
            b =>
            {
                b.Property(p => p.Value).HasColumnName("status");
            });
        
        builder.Property(a => a.CreatedAt) 
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
            .HasColumnName("upated_at");

    }
}