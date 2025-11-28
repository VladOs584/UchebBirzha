using Microsoft.Extensions.DependencyInjection;
using UchebBirzha.Application.Interfaces;
using UchebBirzha.Application.Services;

namespace UchebBirzha.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
       
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}