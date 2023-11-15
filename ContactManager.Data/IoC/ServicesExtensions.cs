using ContactManager.Data.Contexts.Contracts;
using ContactManager.Data.Contexts.Implementation;
using ContactManager.Data.Repositories.Contracts;
using ContactManager.Data.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Data.IoC
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlDbConnection")),
            ServiceLifetime.Transient);
        }

        public static void ConfigureDbContext(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}