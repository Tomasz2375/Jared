namespace Jared.Domain.Enums;

[Flags]
public enum TaskStatus
{
    None = 0,
    Created = 1,
    ToDo = 2,
    Doing = 4,
    Done = 8,
    Blocked = 16,
    Canceled = 32,
}
