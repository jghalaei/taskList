using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Net.Security;
using Task.Application.Accessors;
using GenericContracts.Contracts;
using Microsoft.Extensions.Configuration;
using MassTransit;
namespace Task.Application;

public static class ApplicationServiceRegisteration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) => cfg.Host(configuration["EventBusSettings:HostAddress"]));
        });
        return services;
    }
}