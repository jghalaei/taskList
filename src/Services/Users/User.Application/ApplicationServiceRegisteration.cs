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

            services.AddScoped<JwtService>(sp => new JwtService(configuration["Jwt:key"], configuration["Jwt:Issuer"], configuration["Jwt:Audience"]));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}