using User.Repository;
using User.Application;
using GenericTools.Logger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseCustomSerilog();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
//////
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Users API");
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();

