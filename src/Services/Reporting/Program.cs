using GenericContracts.Contracts;
using GenericContracts.EventBusMessages;
using MassTransit;
using Reporting.Data;
using Reporting.Endpoints;
using Reporting.Entities;
using Reporting.EventBusConsumer;
using Reporting.Repositories;
using GenericTools.Logger;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseCustomSerilog();
// Add services to the container.
builder.Services.AddScoped<IReportContext, ReportContext>();
builder.Services.AddScoped<IReportRepository, ReportingRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<TaskCreatedConsumer>();
    config.AddConsumer<TaskUpdatedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.TaskCreatedQueue, c =>
        {
            c.ConfigureConsumer<TaskCreatedConsumer>(ctx);
        });
        cfg.ReceiveEndpoint(EventBusConstants.TaskUpdatedQueue, c =>
        {
            c.ConfigureConsumer<TaskUpdatedConsumer>(ctx);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapReportingEndpoints();

app.Run();

