using FluentValidation;

namespace Arabam.Com.Application.Adverts.Queries.GetAdvertsById
{
    public class GetAdvertsByIdQueryValidator : AbstractValidator<GetAdvertsByIdQuery>
    {
        public GetAdvertsByIdQueryValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
