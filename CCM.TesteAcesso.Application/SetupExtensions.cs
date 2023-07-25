using CCM.TesteAcesso.Application.Configurations;
using CCM.TesteAcesso.Application.Services.CreateTransfer;
using CCM.TesteAcesso.Application.Services.GetTransfer;
using CCM.TesteAcesso.Application.Services.ProcessTransfer;
using CCM.TesteAcesso.Application.Services.ValidateTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CCM.TesteAcesso.Application
{
    public static class ApplicationSetupExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreateTransferService, CreateTransferService>();
            services.AddScoped<IGetTransferService, GetTransferService>();
            services.AddScoped<IValidateTransferService, ValidateTransferService>();
            services.AddScoped<IProcessTransferService, ProcessTransferService>();

            services.AddMassTransit(configuration);

            return services;
        }
    }
}
