using GenericContracts.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Task.Repository.Db;
using Task.Core.Entities;
using Task.Repository.Repositories;
namespace User.Repository
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:TaskDb"]);
            });
            services.AddScoped<IRepository<TodoTask>, TodoTaskRepository>();
            services.AddScoped<IRepository<TaskHistory>, TaskHistoryRepository>();

            return services;
        }
    }
}