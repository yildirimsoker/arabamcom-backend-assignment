using Arabam.Com.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Arabam.Com.Application.AdvertVisits.Commands.CreateAdvertVisit
{
    public class CreateAdvertVisitCommand : IRequest<int>
    {
        public int AdvertId { get; set; }
        public string? IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }

    public class CreateAdvertVisitCommandHandler : IRequestHandler<CreateAdvertVisitCommand, int>
    {
        private readonly IAdvertVisitsRepository _advertVisitsRepository;
        private readonly IMapper _mapper;

        public CreateAdvertVisitCommandHandler(IMapper mapper,IAdvertVisitsRepository advertVisitsRepository)
        {
            _advertVisitsRepository = advertVisitsRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAdvertVisitCommand request, CancellationToken cancellationToken)
        {
            return await _advertVisitsRepository.AddAsync(_mapper.Map<Arabam.Com.Domain.Entities.AdvertVisits>(request));
        }
    }
}
