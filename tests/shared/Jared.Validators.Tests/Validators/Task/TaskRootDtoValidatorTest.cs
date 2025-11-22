using FluentValidation.TestHelper;
using Jared.Dtos.Tasks;
using Jared.Shared.Enums;
using Jared.Validators.Task;
using TaskStatus = Jared.Shared.Enums.TaskStatus;

namespace Jared.Shared.Tests.Validators.Task;

public class TaskRootDtoValidatorTest
{
    private readonly TaskRootDtoValidator validator = new();

    [Fact]
    public void Validation_WhenDtoIsValid_ShouldNotHaveAnyValidationErrors()
    {
        // Arrange
        var dto = validDto();

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [MemberData(nameof(TitleMemberData))]
    public void Validation_WhenTitleIsNotValid_ShouldReturnValidationError(string title)
    {
        // Arrange
        var dto = validDto();
        dto.Title = title;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Theory]
    [MemberData(nameof(ProjectMemberData))]
    public void Validation_WhenProjectIdIsNotValidEnumValue_ShouldReturnValidationError(int projectId)
    {
        // Arrange
        var dto = validDto();
        dto.ProjectId = projectId;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProjectId);
    }

    [Theory]
    [MemberData(nameof(StatusMemberData))]
    public void Validation_WhenStatusIsNotValidEnumValue_ShouldReturnValidationError(TaskStatus status)
    {
        // Arrange
        var dto = validDto();
        dto.Status = status;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [Theory]
    [MemberData(nameof(PriorityMemberData))]
    public void Validation_WhenPriorityIsNotValidEnumValue_ShouldReturnValidationError(Priority priority)
    {
        // Arrange
        var dto = validDto();
        dto.Priority = priority;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Priority);
    }

    private TaskRootDto validDto() => new()
    {
        Id = 1,
        Title = "Task title",
        ProjectId = 2,
        Code = "CD-1",
        EpicId = 3,
        CreatedById = 10,
        AssignedToId = 11,
        Status = TaskStatus.Done,
        Priority = Priority.Normal,
        CreatedAt = new(2025, 3, 4, 19, 57, 22),
        Deadline = new(2025, 3, 31),
    };

    public static IEnumerable<object[]> TitleMemberData()
    {
        yield return new object[] { null! };
        yield return new object[] { string.Empty };
        yield return new object[] { string.Concat(Enumerable.Repeat(".", 101)) };
    }

    public static IEnumerable<object[]> ProjectMemberData()
    {
        yield return new object[] { null! };
        yield return new object[] { 0 };
        yield return new object[] { -1 };
    }

    public static IEnumerable<object[]> StatusMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 64 };
    }

#pragma warning disable S4144 // Methods should not have identical implementations
    public static IEnumerable<object[]> PriorityMemberData()
#pragma warning restore S4144 // Methods should not have identical implementations
    {
        yield return new object[] { -1 };
        yield return new object[] { 64 };
    }
}
