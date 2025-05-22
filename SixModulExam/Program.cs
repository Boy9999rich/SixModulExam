
using SixModulExam.Configurations;
using SixModulExam.EndPoints;
using SixModulExam.Middleware;

namespace SixModulExam;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Configuration();
        builder.ConfigurationJwtAuth();
        builder.ConfigureJwtSettings();
        builder.ConfigureSerilog();
        builder.Services.ConfigureDataBase();


        var app = builder.Build();
        app.MapAuthEndpoints();
        app.MapUserEndpoints();
        app.MapContactEndpoints();
        app.MapRoleEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
