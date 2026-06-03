using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoJarvis.Domain.Interfaces;
using ProjetoJarvis.Infraestructure.Context;
using ProjetoJarvis.Infraestructure.Repositories;

namespace ProjetoJarvis.Infraestructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IIntegrationRepository, IntegrationRepository>();

            return services;
        }
    }
}