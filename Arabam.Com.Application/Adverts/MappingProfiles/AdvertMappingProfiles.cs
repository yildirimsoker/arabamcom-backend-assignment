using Arabam.Com.Application.Adverts.Queries.GetAdvertsById;
using Arabam.Com.Application.Adverts.Queries.GetAdvertsWithPagination;
using AutoMapper;

namespace Arabam.Com.Application.Adverts.MappingProfiles
{
    public class AdvertMappingProfiles: Profile
    {
        public AdvertMappingProfiles()
        {
            CreateMap<Arabam.Com.Domain.Entities.Adverts, GetAdvertsByIdQueryDTO>();
            CreateMap<Arabam.Com.Domain.Entities.Adverts, GetAdvertsWithPaginationDTO>();
        }
    }
}
