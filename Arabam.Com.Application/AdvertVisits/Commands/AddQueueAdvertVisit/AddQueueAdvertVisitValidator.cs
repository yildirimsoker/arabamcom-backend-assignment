using Arabam.Com.Application.Common.Interfaces;
using FluentValidation;

namespace Arabam.Com.Application.AdvertVisits.Commands.AddQueueAdvertVisit
{
    public class AddQueueAdvertVisitValidator : AbstractValidator<AddQueueAdvertVisitCommand>
    {
        private readonly IAdvertRepository _advertRepository;

        public AddQueueAdvertVisitValidator(IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;

            RuleFor(v => v.AdvertId)
               .GreaterThan(0).WithMessage("AdvertId must be greater than zero.")
               .MustAsync(AnyAdvert).WithMessage("Advert not found.");

            RuleFor(v => v.IPAddress)
                .NotEmpty().WithMessage("IPAddress is required.")
                .MaximumLength(45).WithMessage("IPAddress must not exceed 45 characters.")
                .Matches(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b")
                .WithMessage("IP Addres is not valid");

        }

        public async Task<bool> AnyAdvert(int advertId, CancellationToken cancellationToken)
        { 
            return await _advertRepository.GetAnyAsync(advertId);
        }
    }
}
