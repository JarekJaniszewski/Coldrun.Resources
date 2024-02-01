using FluentValidation;
using Resources.Application.Validation;
using Resources.Domain.Enums;

namespace Resources.Application.TruckManagement.Commands.UpdateTruck
{
    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
    {
        public UpdateTruckCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(ValidationMessages.NotEmpty)
                .Matches(Constants.AlphanumericRegex)
                .WithName(nameof(UpdateTruckCommand.Code))
                .WithMessage(ValidationMessages.AlphaNumericValidationMessage);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.NotEmpty);

            RuleFor(x => x.Status)
                .Must(i => Enum.IsDefined(typeof(StatusType), i))
                .WithMessage(ValidationMessages.NotEmpty);
        }
    }
}
