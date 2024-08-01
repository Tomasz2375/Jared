using Jared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jared.Infrastructure.ModelConfiguration;

public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.HasOne(th => th.Task)
            .WithMany(t => t.TaskHistories)
            .HasForeignKey(th => th.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(th => th.User)
            .WithMany(u => u.TaskHistories)
            .HasForeignKey(th => th.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");
    }
}
