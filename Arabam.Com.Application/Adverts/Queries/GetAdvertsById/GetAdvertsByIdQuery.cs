using Arabam.Com.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Arabam.Com.Application.Adverts.Queries.GetAdvertsById
{
    public class GetAdvertsByIdQuery : IRequest<GetAdvertsByIdQueryDTO>
    {
        public int Id { get; set; }
    }

    public class GetAdvertsByIdQueryHandler : IRequestHandler<GetAdvertsByIdQuery, GetAdvertsByIdQueryDTO>
    {

        private readonly IMapper _mapper;
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertsByIdQueryHandler(IMapper mapper, IAdvertRepository advertRepository)
        {
            _mapper = mapper;
            _advertRepository = advertRepository;
        }

        public async Task<GetAdvertsByIdQueryDTO> Handle(GetAdvertsByIdQuery request, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetAsync(request.Id);
            return _mapper.Map<GetAdvertsByIdQueryDTO>(advert);
        }
    }
}
