using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleService.Database.Configurations;

public class ArticlesPendingConfiguration : IEntityTypeConfiguration<ArticlesPending>
{
    public void Configure(EntityTypeBuilder<ArticlesPending> builder)
    {
        builder.ToTable("articles_pending");

        builder.HasKey(ap => ap.Id);
        builder.Property(ap => ap.Id).HasColumnName("id");

        builder.Property(ap => ap.ArticleId)
            .HasColumnName("article_id");
        
        builder
            .Property(ap => ap.RejectionMessage)
            .HasColumnName("rejection_message")
            .HasMaxLength(512);

        builder.Property(ap => ap.Reviewed)
            .HasColumnName("reviewed");

        builder.ComplexProperty(
            ap => ap.PublicationTime,
            b =>
            {
                b.Property(p => p.Start).HasColumnName("start_date");
                b.Property(p => p.End).HasColumnName("end_date");
            });

        builder.Property(ap => ap.Reviewer).HasColumnName("reviewer");
    }
}