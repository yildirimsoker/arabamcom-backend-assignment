using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Arabam.Com.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {

        private readonly ILogger _logger;


        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogInformation("Arabam.com Request: {Name} {@Request}",
                    requestName, request);
            });

        }
    }
}
