using AutoFixture.NUnit3;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Resources.Application.TruckManagement.Commands.DeleteTruck;

namespace Resources.Application.UnitTests.Validation
{
    public class DeleteTruckCommandValidatorTests
    {
        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        [InlineAutoData(" ")]
        [InlineAutoData("ASSA1212--1221")]
        public void Validate_WithInvalidCode_ShouldReturnCorrectErrorMessage(
            string code,
            DeleteTruckCommand command,
            DeleteTruckCommandValidator sut)
        {
            // Arrange
            command.Code = code;

            // Act
            var result = sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }
    }
}
