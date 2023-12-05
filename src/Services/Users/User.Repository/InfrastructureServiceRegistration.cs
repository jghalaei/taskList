using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using Microsoft.Extensions.DependencyInjection;
using User.Core.Entities;
using User.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using User.Repository.Db;
namespace User.Repository
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(options =>
            {
                //  "Server=localhost;Port=5432;Database=UsersDb;User Id=admin;Password=admin1234;"
                string server= configuration["UsersDb:Server"]?? "";
                string port = configuration["UsersDb:Port"] ?? "";
                string database = configuration["UsersDb:Database"] ?? "";
                string userId = configuration["UsersDb:UserId"] ?? "";
                string Password = configuration["UsersDb:Password"] ?? "";
                string connectionString = $"Server={server};Port={port};Database={database};User Id={userId};Password={Password};";
                options.UseNpgsql(connectionString);
                

            });
            
            services.AddScoped<IRepository<AppUser>, UserRepository>();
            return services;
        }
    }
}