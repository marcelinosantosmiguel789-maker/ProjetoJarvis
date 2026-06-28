using Microsoft.Extensions.DependencyInjection;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Application.Mappings;
using ProjetoJarvis.Application.Services;

namespace ProjetoJarvis.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Application Services
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMemoryService, MemoryService>();
            services.AddScoped<IIntegrationService, IntegrationService>();

            return services;
        }
    }
}
