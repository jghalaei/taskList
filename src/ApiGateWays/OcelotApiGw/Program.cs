
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using GenericTools.Logger;
using OcelotApiGw;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseCustomSerilog();
        builder.Services.AddHttpContextAccessor();
        builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");
        // builder.Logging.AddConsole();
        // builder.Logging.AddDebug();
        builder.Services.AddHealthChecks();
        var authenticationProviderKey = "AuthKey";
        builder.Services.AddAuthentication()
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
                };
            });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                bld => bld.WithOrigins(builder.Configuration["AllowedCors"])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                    );
        });

        builder.Services.AddOcelot()
        .AddDelegatingHandler<CorrelationIdDelgatingHandler>();

        var app = builder.Build();
        app.UseCors("CorsPolicy");
        app.MapGet("/", () => "Hello World!");
        app.UseHealthChecks("/health");
        app.UseOcelot().Wait();
        app.Run();
    }
}