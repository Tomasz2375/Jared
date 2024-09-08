namespace Jared.Shared.Enums;

[Flags]
public enum EpicStatus
{
    None = 0,
    Created = 1,
    ToDo = 2,
    Doing = 4,
    Done = 8,
    Blocked = 16,
    Canceled = 32,
}
