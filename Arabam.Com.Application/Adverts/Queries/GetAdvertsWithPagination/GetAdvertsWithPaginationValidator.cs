using FluentValidation;

namespace Arabam.Com.Application.Adverts.Queries.GetAdvertsWithPagination
{
    public class GetAdvertsWithPaginationValidator: AbstractValidator<GetAdvertsWithPaginationQuery>
    {
        public GetAdvertsWithPaginationValidator()
        {

            RuleFor(v => v.Price)
                .Must(x=> x == null || x > 0).WithMessage("Price must be greater than zero.");

            RuleFor(v => v.CategoryId)
                .Must(x => x == null || x > 0).WithMessage("CategoryId must be greater than zero.");

            RuleFor(v => v.Fuel)
                .MaximumLength(32).WithMessage("Fuel must not exceed 32 characters.");

            RuleFor(v => v.Gear)
                .MaximumLength(32).WithMessage("Gear must not exceed 32 characters.");

            RuleFor(v => v.Page)
                .GreaterThan(0).WithMessage("Page must be greater than zero.");

            RuleFor(v => v.SortingField)
                .Must(x => x == null
                        || x.Equals("price", StringComparison.OrdinalIgnoreCase) 
                        || x.Equals("year", StringComparison.OrdinalIgnoreCase)
                        || x.Equals("km", StringComparison.OrdinalIgnoreCase)
                        || x.Equals("Id", StringComparison.OrdinalIgnoreCase))
                .WithMessage("You can only sorting for km, price, year and Id.");

            RuleFor(v => v.SortingType)
                .Must(x => x == null 
                        || x.Equals("asc", StringComparison.OrdinalIgnoreCase)
                        || x.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("SortingType must be desc or asc.");
        }
    }
}
