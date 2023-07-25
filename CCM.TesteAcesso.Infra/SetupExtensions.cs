using CCM.TesteAcesso.Domain.Interfaces.Repository;
using CCM.TesteAcesso.Infra.Clients;
using CCM.TesteAcesso.Infra.Clients.Account;
using CCM.TesteAcesso.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiraeDigital.InvestmentFunds.Infrastructure.Http;
using Refit;

namespace CCM.TesteAcesso.Infra
{
    public static class InfrastructureConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(option =>
            {
                var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
                option.UseNpgsql(connectionString!);
            });

            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();

            services.AddScoped<IRefitHttpWrapper, RefitHttpWrapper>();
            services.AddRefitClient<IAccountClient>()
            .ConfigureHttpClient(c =>
            {
                var baseAdress = configuration.GetSection("AccountClient:BaseAddress").Value;
                c.BaseAddress = new Uri(baseAdress ?? throw new InvalidOperationException());
            });


            return services;
        }

        public static void UpgradeDatabase(this Context context)
        {
            if (context is { Database: { } })
            {
                context.Database.Migrate();
            }
        }
    }
}
