using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration config)
        {
            var connStr = config.GetConnectionString("Default");
            services.AddDbContext<ClinicContext>(opts =>
                opts.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        public static IServiceCollection AddAivenDb(this IServiceCollection services, IConfiguration config)
        {
            var connStr = config.GetConnectionString("AivenDbClinic");
            services.AddDbContext<ClinicContext>(opts =>
                opts.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
