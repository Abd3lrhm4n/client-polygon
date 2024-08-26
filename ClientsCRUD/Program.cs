using Domain.IRepositories;
using Infrastructure.Repositories;
using Domain.IRepositories.Common;
using Infrastructure.Repositories.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.Services.Client;
using Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllLocalhosts",
        policy => policy.WithOrigins("http://localhost", "http://localhost:4200", "http://127.0.0.1")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IClientRepository), typeof(ClientRepository));
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


//builder.Services.AddScoped()
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllLocalhosts");

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
