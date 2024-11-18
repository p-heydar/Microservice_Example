using ElectronBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronBoard.Infrastructure.SqlServer.Persistence.Configurations.CandidateVotes;

public sealed class CandidateVoteConfiguration:IEntityTypeConfiguration<ElectionCycleVote>
{
    public void Configure(EntityTypeBuilder<ElectionCycleVote> builder)
    {
        builder.HasKey(x => new { x.ElectionCycle, x.FipsCode });

        builder.Property(x => x.FipsCode)
            .IsRequired();

        builder.Property(x => x.ElectionCycle)
            .IsRequired();
    }
}