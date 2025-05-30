using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace APBD_6.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private static ILoggerFactory? _loggerFactory;

    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var enableDebugSql = args.Contains("--debug-sql");
        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        if (enableDebugSql)
        {
            _loggerFactory ??= LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                    .AddConsole()
                    .AddDebug();
            });

            optionsBuilder
                .UseSqlServer(connectionString, opt =>
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        return new AppDbContext(optionsBuilder.Options);
    }
}