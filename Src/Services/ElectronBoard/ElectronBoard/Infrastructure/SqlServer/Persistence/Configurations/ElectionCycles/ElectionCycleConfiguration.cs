using ElectronBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronBoard.Infrastructure.SqlServer.Persistence.Configurations.ElectionCycles;

public class ElectionCycleConfiguration:IEntityTypeConfiguration<ElectionCycle>
{
    public void Configure(EntityTypeBuilder<ElectionCycle> builder)
    {
        builder.HasKey(x => x.Year);
    }
}