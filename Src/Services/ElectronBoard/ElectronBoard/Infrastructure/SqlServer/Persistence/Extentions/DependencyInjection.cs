using ElectronBoard.Infrastructure.Redis;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ElectronBoard.Infrastructure.SqlServer.Persistence.Extentions;

public static class DependencyInjection
{
    public static IServiceCollection InitialDataBase(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ElectionDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ElectionDbContext.ConnectionString));
        });

        service.AddSingleton<IConnectionMultiplexer>(config =>
            ConnectionMultiplexer.Connect("localhost:6379,password=my-password"));        
        service.AddSingleton<VoteRepository>();
        
        return service;
    }
}