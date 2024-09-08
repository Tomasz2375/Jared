using Jared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jared.Infrastructure.ModelConfiguration;

public class EpicConfiguration : IEntityTypeConfiguration<Epic>
{
    public void Configure(EntityTypeBuilder<Epic> builder)
    {
        builder.HasOne(e => e.Project)
            .WithMany(p => p.Epics)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");
        builder.Property(x => x.ProjectId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
    }
}
