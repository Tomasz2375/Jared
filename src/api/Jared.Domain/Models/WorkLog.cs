using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

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
