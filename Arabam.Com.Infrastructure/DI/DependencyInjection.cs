using Arabam.Com.Application.Common.Interfaces;
using Arabam.Com.Infrastructure.RabbitMQ;
using Arabam.Com.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Arabam.Com.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAdvertVisitsRepository, AdvertVisitsRepository>();
            services.AddTransient<IAdvertRepository, AdvertRepository>();
            services.AddTransient<IMessageService, MessageService>();

            return services;
        }
    }
}
