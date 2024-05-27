using Labsit.Application;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Infrastructure.Context;
using Labsit.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();
            services.AddInfrastructure();
            services.AddCommandHandlers();
            return services;
        }

        internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LabsitContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("LabsitConnection"));
            });

            return services;
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IBankAccountRepository, BankAccountRepository>()
                .AddScoped<ICardRepository, CardRepository>();
        }

        internal static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
