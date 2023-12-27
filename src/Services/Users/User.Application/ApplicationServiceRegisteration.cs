using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
namespace User.Application
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<JwtService>(sp => new JwtService(configuration["Jwt:key"] ?? throw new ArgumentNullException("Jwt key not found")
                    , configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt Issuer not found")
                    , configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt Audience not found")));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}