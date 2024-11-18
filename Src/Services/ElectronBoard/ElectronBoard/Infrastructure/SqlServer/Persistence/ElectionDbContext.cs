using ElectronBoard.Domain.Entities;
using ElectronBoard.Infrastructure.SqlServer.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;

namespace ElectronBoard.Infrastructure.SqlServer.Persistence;

public class ElectionDbContext:DbContext
{
    public ElectionDbContext(DbContextOptions options) : base(options)
    {
    }
    // ConnectionString
    public const string ConnectionString = "ElectronConnectionString";
    
    public DbSet<ElectionCycleVote> ElectionCycleVotes { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<ElectionCycle> ElectionCycles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding((context, _) =>
                {
                    DbSet<State> StateDbSet = context.Set<State>();

                    if (StateDbSet.Any())
                    {
                        foreach (var item in StateSeedData.GetAll())
                        {
                            StateDbSet.Add(item);
                        }
                        context.SaveChanges();
                    }           
                })
            .UseAsyncSeeding(async (context, _, CancellationToken) =>
                {
                    DbSet<State> StateDbSet = context.Set<State>();

                    if (! await StateDbSet.AnyAsync())
                    {
                        foreach (var item in StateSeedData.GetAll())
                        {
                            await StateDbSet.AddAsync(item, CancellationToken);
                        }
                        await context.SaveChangesAsync(CancellationToken);
                    }
                });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElectionDbContext).Assembly);
    }
}