using FluentValidation;

namespace Resources.Application.TruckManagement.Queries.TrucksSearch
{
    public class TrucksSearchQueryCommandValidator : AbstractValidator<TrucksSearchQueryCommand>
    {
        public TrucksSearchQueryCommandValidator()
        {
            When(x => x.Paging != null, () =>
            {
                RuleFor(x => x.Paging.Page)
                    .GreaterThan(0)
                    .WithMessage("Page number must be greater than 0");

                RuleFor(x => x.Paging.PageSize)
                    .GreaterThan(0)
                    .WithMessage("Page size must be greater than 0");
            });
        }
    }
}
