using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jared.Infrastructure.ModelConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Models.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
    {
        builder.HasOne(t => t.Epic)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EpicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.CreatedBy)
            .WithMany(u => u.CreatedTask)
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedTask)
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

        builder.Property(x => x.ProjectId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Code).HasMaxLength(10);
    }
}
