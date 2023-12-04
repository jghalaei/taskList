using GenericContracts.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Task.Repository.Db;
using Task.Core.Entities;
using Task.Repository.Repositories;
namespace Task.Repository;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskContext>(options =>
         {
                string server= configuration["TaskDb:Server"]?? "";
                string port = configuration["TaskDb:Port"] ?? "";
                string database = configuration["TaskDb:Database"] ?? "";
                string userId = configuration["TaskDb:UserId"] ?? "";
                string Password = configuration["TaskDb:Password"] ?? "";
                string connectionString = $"Server={server};Port={port};Database={database};User Id={userId};Password={Password};";

             options.UseNpgsql(connectionString);
         });
        services.AddScoped<IRepository<TodoTask>, TodoTaskRepository>();
        services.AddScoped<IRepository<TaskHistory>, TaskHistoryRepository>();

        return services;
    }

    
}