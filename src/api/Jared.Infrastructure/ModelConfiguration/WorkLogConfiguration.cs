using Jared.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jared.Infrastructure.ModelConfiguration;

public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
{
    public void Configure(EntityTypeBuilder<WorkLog> builder)
    {
        builder.HasOne(wl => wl.Task)
            .WithMany(t => t.WorkLogs)
            .HasForeignKey(wl => wl.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(th => th.User)
            .WithMany(u => u.WorkLogs)
            .HasForeignKey(th => th.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
