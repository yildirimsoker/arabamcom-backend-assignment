using Arabam.Com.Application.AdvertVisits.Commands.CreateAdvertVisit;
using AutoMapper;

namespace Arabam.Com.Application.AdvertVisits.MappingProfiles
{
    public class AdvertVisitsMappingProfiles : Profile
    {
        public AdvertVisitsMappingProfiles()
        {
            CreateMap<CreateAdvertVisitCommand, Arabam.Com.Domain.Entities.AdvertVisits>();
        }
    }
}
