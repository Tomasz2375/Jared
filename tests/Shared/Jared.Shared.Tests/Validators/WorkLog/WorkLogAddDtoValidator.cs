using FluentValidation.TestHelper;
using Jared.Shared.Dtos.WorkLogDtos;
using Jared.Shared.Validators.WorkLog;

namespace Jared.Shared.Tests.Validators.WorkLog;

public class WorkLogAddDtoValidatorTest
{
    private readonly WorkLogAddDtoValidator validator = new();

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
    [MemberData(nameof(HoursMemberData))]
    public void Validation_WhenHoursIsNotValid_ShouldReturnValidationError(int hours)
    {
        // Arrange
        var dto = validDto();
        dto.Hours = hours;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Hours);
    }

    [Theory]
    [MemberData(nameof(MinutesMemberData))]
    public void Validation_WhenMinutesIsNotValid_ShouldReturnValidationError(int minutes)
    {
        // Arrange
        var dto = validDto();
        dto.Minutes = minutes;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Minutes);
    }

    [Theory]
    [MemberData(nameof(WorkDateMemberData))]
    public void Validation_WhenWorkDateIsNotValid_ShouldReturnValidationError(DateTime? workDate)
    {
        // Arrange
        var dto = validDto();
        dto.WorkDate = workDate;

        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WorkDate);
    }

    private WorkLogAddDto validDto() => new()
    {
        Hours = 1,
        Minutes = 30,
        WorkDate = DateTime.Now.Date,
    };

    public static IEnumerable<object[]> HoursMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 24 };
    }

    public static IEnumerable<object[]> MinutesMemberData()
    {
        yield return new object[] { -1 };
        yield return new object[] { 60 };
    }

    public static IEnumerable<object[]> WorkDateMemberData()
    {
        yield return new object[] { null! };
        yield return new object[] { DateTime.Now.Date.AddDays(1) };
        yield return new object[] { DateTime.Now.Date.AddMonths(-1) };
    }
}
