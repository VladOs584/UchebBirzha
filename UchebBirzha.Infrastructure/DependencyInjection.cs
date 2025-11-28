using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;
using UchebBirzha.Infrastructure.Repositories;
using UchebBirzha.Infrastructure.Interfaces;
using UchebBirzha.Infrastructure.Services;

namespace UchebBirzha.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        
            services.AddScoped<IUnitOfWork, UnitOfWork>();

       
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}