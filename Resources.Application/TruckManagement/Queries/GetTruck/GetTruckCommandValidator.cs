using FluentValidation;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using Resources.Application.Validation;


namespace Resources.Application.TruckManagement.Queries.GetTruck
{
    public class GetTruckCommandValidator : AbstractValidator<GetTruckCommand>
    {
        public GetTruckCommandValidator()
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
