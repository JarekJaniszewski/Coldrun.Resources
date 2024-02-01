using FluentValidation;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using Resources.Application.Validation;

namespace Resources.Application.TruckManagement.Commands.DeleteTruck
{
    public class DeleteTruckCommandValidator : AbstractValidator<DeleteTruckCommand>
    {
        public DeleteTruckCommandValidator()
        {
            RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmpty)
            .Matches(Constants.AlphanumericRegex)
            .WithName(nameof(CreateTruckCommand.Code))
            .WithMessage(ValidationMessages.AlphaNumericValidationMessage);
        }
    }
}
