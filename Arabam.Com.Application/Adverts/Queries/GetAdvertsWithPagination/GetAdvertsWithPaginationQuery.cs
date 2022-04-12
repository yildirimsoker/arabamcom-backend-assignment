using Arabam.Com.Application.Common.Constant;
using Arabam.Com.Application.Common.Interfaces;
using Arabam.Com.Application.Common.Model;
using AutoMapper;
using MediatR;

namespace Arabam.Com.Application.Adverts.Queries.GetAdvertsWithPagination
{
    public class GetAdvertsWithPaginationQuery : IRequest<PaginatedList<GetAdvertsWithPaginationDTO>>
    {
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Gear { get; set; }
        public string? Fuel { get; set; }
        public int Page { get; set; } = PaginationConstant.DefaultPage;
        public string SortingField { get; set; } = PaginationConstant.DefaultSortingField;
        public string SortingType { get; set; } = PaginationConstant.DefaultSortingType;
    }

    public class GetAdvertsWithPaginationQueryHandler : IRequestHandler<GetAdvertsWithPaginationQuery, PaginatedList<GetAdvertsWithPaginationDTO>>
    {

        private readonly IMapper _mapper;
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertsWithPaginationQueryHandler(IMapper mapper, IAdvertRepository advertRepository)
        {
            _mapper = mapper;
            _advertRepository = advertRepository;
        }

        public async Task<PaginatedList<GetAdvertsWithPaginationDTO>> Handle(GetAdvertsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            Domain.Common.Filter filter = new Domain.Common.Filter();

            filter.OrderByColumnName = request.SortingField;
            filter.OrderBy = request.SortingType.Equals ("Desc", StringComparison.OrdinalIgnoreCase) ? Domain.Enums.OrderByType.Desc : Domain.Enums.OrderByType.Asc;
            filter.Offset = (request.Page - 1) * PaginationConstant.DefaultLimit;
            filter.Next = PaginationConstant.DefaultLimit;

            if (request.CategoryId.HasValue)
            {
                filter.Where.Add(nameof(request.CategoryId), request.CategoryId);
            }
            if (request.Price.HasValue)
            {
                filter.Where.Add(nameof(request.Price), request.Price);
            }
            if (!string.IsNullOrWhiteSpace(request.Gear))
            {
                filter.Where.Add(nameof(request.Gear), request.Gear);
            }
            if (!string.IsNullOrWhiteSpace(request.Fuel))
            {
                filter.Where.Add(nameof(request.Fuel), request.Fuel);
            }
           
            var advertsList = _mapper.Map<List<GetAdvertsWithPaginationDTO>>(await _advertRepository.GetAllAsync(filter));
            int advertsCount = await _advertRepository.GetCountAsync(filter);

            PaginatedList<GetAdvertsWithPaginationDTO> paginatedList = new PaginatedList<GetAdvertsWithPaginationDTO>(advertsList, advertsCount, request.Page, PaginationConstant.DefaultLimit);
           
            return paginatedList;

        }
    }
}
