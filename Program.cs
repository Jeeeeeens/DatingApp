using System.Text;
using API.Data;
using API.errors;
using API.extensions;
using API.Interfaces;
using API.middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
  private static async Task Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplicationServices(builder.Configuration); // extension method -> zie folder
    builder.Services.AddIdentityServices(builder.Configuration); // extension method -> zie folder


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseMiddleware<ExceptionMiddleware>();
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    

    app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

  
    //BELANGRIJK: eerst AUTHENTICATION, DAN PAS AUTHORIZATION  
    app.UseAuthentication();
    app.UseAuthorization();
  
    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
    }
    catch(Exception ex)
    {
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
    }

    app.Run();
  }
}