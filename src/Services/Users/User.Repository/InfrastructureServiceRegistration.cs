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
                options.UseNpgsql(configuration["ConnectionStrings:UsersDb"]);
            });
            services.AddScoped<IRepository<AppUser>, UserRepository>();
            return services;
        }
    }
}