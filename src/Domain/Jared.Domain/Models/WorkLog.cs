using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Task = Jared.Domain.Models.Task;

namespace Jared.Domain.Model;

public class WorkLog : Entity
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public TimeSpan Time { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime LoggedDate { get; set; }

    public Task? Task { get; set; }
    public User? User { get; set; }
}
