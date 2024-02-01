using AutoFixture.NUnit3;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using Resources.Domain.Enums;

namespace Resources.Application.UnitTests.Validation
{
    public class CreateTruckCommandValidatorTests
    {
        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        [InlineAutoData(" ")]
        [InlineAutoData("ASSA1212--1221")]
        public void Validate_WithInvalidCode_ShouldReturnCorrectErrorMessage(
            string code,
            CreateTruckCommand command,
            CreateTruckCommandValidator sut)
        {
            // Arrange
            command.Code = code;

            // Act
            var result = sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        [InlineAutoData(" ")]
        public void Validate_WithInvalidName_ShouldReturnCorrectErrorMessage(
            string name,
            CreateTruckCommand command,
            CreateTruckCommandValidator sut)
        {
            // Arrange
            command.Code = command.Code.Replace("-", "");
            command.Name = name;

            // Act
            var result = sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineAutoData(-1)]
        [InlineAutoData(5)]
        public void Validate_WithInvalidStatus_ShouldReturnCorrectErrorMessage(
            int status,
            CreateTruckCommand command,
            CreateTruckCommandValidator sut)
        {
            // Arrange
            command.Code = command.Code.Replace("-", "");
            command.Status = (StatusType)status;

            // Act
            var result = sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}
