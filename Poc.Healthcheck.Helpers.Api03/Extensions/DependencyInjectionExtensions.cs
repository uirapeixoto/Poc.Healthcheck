using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Poc.Healthcheck.Helpers.Infra.Data.Entities;
using Poc.Healthcheck.Helpers.Infra.HttpServices;
using Poc.Helalthcheck.Helpers.Domain.Interface.services;
using System.Data;

namespace Poc.Healthcheck.Helpers.Api03.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void IOC(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServices();
            services.RegisterDataBase(configuration);
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IViaCepService, ViaCEPHttpService>();
        }

        public static void RegisterDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BDTesteContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Default"))
                );
            services.AddScoped<IDbConnection>(conn => new SqlConnection(configuration.GetConnectionString("Default")));
        }
    }
}
