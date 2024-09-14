using FluentValidation.TestHelper;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Enums;
using Jared.Shared.Validators.Epic;

namespace Jared.Shared.Tests.Validators.Epic;

public class EpicRootDtoValidatorTest
{
    private readonly EpicRootDtoValidator validator = new();

    [Fact]
    public void Validation_WhenDtoIsValid_ShouldNotReturnValidationError()
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

    [Fact]
    public void Validation_WhenProjectNotSet_ShouldReturnValidationError()
    {
        // Arrange
        var dto = validDto();
        dto.ProjectId = 0;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProjectId);
    }

    [Theory]
    [MemberData(nameof(StatusMemberData))]
    public void Validation_WhenStatusIsNotValidEnumValue_ShouldReturnValidationError(EpicStatus status)
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
    [MemberData(nameof(ParentIdMemberData))]
    public void Validation_WhenParentIdIsNotValid_ShouldReturnValidationError(int? parentId)
    {
        // Arrange
        var dto = validDto();
        dto.ParentId = parentId;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ParentId);
    }

    private EpicRootDto validDto() => new()
    {
        Id = 1,
        Title = "Valid epic",
        ParentId = null,
        ProjectId = 1,
        Status = EpicStatus.Created,
    };

    public static IEnumerable<object[]> TitleMemberData()
    {
        yield return new object[] { string.Empty };
        yield return new object[] { string.Concat(Enumerable.Repeat(".", 101)) };
    }

    public static IEnumerable<object[]> StatusMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 64 };
    }

    public static IEnumerable<object[]> ParentIdMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 0 };
    }
}
