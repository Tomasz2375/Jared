namespace Jared.Domain.Enums;

[Flags]
public enum Priority
{
    None = 0,
    Low = 1,
    Normal = 2,
    High = 4,
    Urgent = 8,
}
