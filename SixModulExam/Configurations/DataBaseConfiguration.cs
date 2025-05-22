using Microsoft.EntityFrameworkCore;
using UserContacts.Dal;

namespace SixModulExam.Configurations;

public static class DataBaseConfiguration
{
    public static void Configuration(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddDbContext<MainContext>(options =>
          options.UseSqlServer(connectionString));

    }
}
