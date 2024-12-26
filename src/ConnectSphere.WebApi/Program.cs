using ConnectSphere.Application;
using ConnectSphere.Infrastructure;
using ConnectSphere.WebApi;
using ConnectSphere.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddWebApi(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors("AllowAll");

Func<IServiceProvider, object> EnvironmentManager(string webRootPath)
{
    throw new NotImplementedException();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();
