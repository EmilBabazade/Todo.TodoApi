using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Todo.TodoApi.DB;
using Todo.TodoApi.Models;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddDbContext<TodoContext>(opts =>
        {
            var conn = builder.Configuration.GetConnectionString("Default");
            opts.UseMySql(conn, ServerVersion.AutoDetect(conn));
        });
        builder.Services.AddScoped<DbContext, TodoContext>(sp =>
        {
            return sp.GetRequiredService<TodoContext>();
        });

        builder.Services.AddScoped<Repo<TodoEntity>>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
            });
            app.MapSwagger();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}