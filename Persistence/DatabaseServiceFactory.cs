using Betalish.Common.Dates;
using Betalish.Persistence.Common;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Betalish.Persistence;

public class DatabaseServiceFactory : IDesignTimeDbContextFactory<DatabaseService>
{
    public DatabaseService CreateDbContext(string[] args)
    {
        var environment = Environment
            .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        //Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {environment}");

        var builder = new ConfigurationBuilder();

        var currentDirectory = Directory.GetCurrentDirectory();

        //Console.WriteLine($"CURRENT_DIR: {currentDirectory}");

        var configuration = builder
            .SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseService>();

        var connectionString = configuration["Database:ConnectionString"];

        //Console.WriteLine($"CONNECTION_STR: {connectionString}");

        optionsBuilder.UseSqlServer(connectionString);

        // Manually construct dependencies,
        // since EF tooling needs a parameterless constructor
        var dateService = new DateService();
        var formatter = new OnSaveFormatter();
        var validator = new OnSaveValidator();
        var createdDateTimeSetter = new CreatedDateTimeSetter(dateService);
        var updatedDateTimeSetter = new UpdatedDateTimeSetter(dateService);

        return new DatabaseService(
            optionsBuilder.Options, 
            formatter, 
            validator, 
            createdDateTimeSetter, 
            updatedDateTimeSetter);
    }
}
