using Arabam.Com.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace Arabam.Com.Application.AdvertVisits.Commands.AddQueueAdvertVisit
{
    public class AddQueueAdvertVisitCommand : IRequest<bool>
    {
        public int AdvertId { get; set; }
        public string? IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }

    public class AddQueueAdvertVisitCommandHandler : IRequestHandler<AddQueueAdvertVisitCommand, bool>
    {

        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public AddQueueAdvertVisitCommandHandler(IMapper mapper, IMessageService messageService)
        {

            _mapper = mapper;
            _messageService = messageService;
        }

        public async Task<bool> Handle(AddQueueAdvertVisitCommand request, CancellationToken cancellationToken)
        {
           return await Task.Run(() =>
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                return _messageService.Enqueue(jsonRequest);
            });
        }
    }
}
