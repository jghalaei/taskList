using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Net.Security;
using Task.Application.Accessors;
using GenericContracts.Contracts;
namespace Task.Application;

public static class ApplicationServiceRegisteration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}