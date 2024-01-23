using TaskManagerWebApi.Services;

namespace TaskManagerWebApi
{
    public static class ControllerServicesExtension
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IClientLocationRepository, ClientLocationRepository>();

            return services;
        }
    }
}
