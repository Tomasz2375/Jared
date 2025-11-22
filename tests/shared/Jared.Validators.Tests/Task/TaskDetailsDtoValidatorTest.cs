using FluentValidation.TestHelper;
using Jared.Dtos.Tasks;
using Jared.Shared.Enums;
using Jared.Validators.Task;
using TaskStatus = Jared.Shared.Enums.TaskStatus;

namespace Jared.Validators.Tests.Task;

public class TaskDetailsDtoValidatorTest
{
    private readonly TaskDetailsDtoValidator validator = new();

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

    [Fact]
    public void Validation_WhenDescriptionIsNotValid_ShouldReturnValidationError()
    {
        // Arrange
        var dto = validDto();
        dto.Description = new string('x', 501);

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [MemberData(nameof(ParentIdMemberData))]
    public void Validation_WhenParentIdIsNotValid_ShouldReturnValidationError(int parentId)
    {
        // Arrange
        var dto = validDto();
        dto.ParentId = parentId;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ParentId);
    }

    private TaskDetailsDto validDto() => new()
    {
        ParentId = null,
        Description = null,

        // valid TaskRootDto
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

    public static IEnumerable<object[]> ParentIdMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 0 };
    }
}
