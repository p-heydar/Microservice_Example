using System.Collections.ObjectModel;

namespace ElectronBoard.Domain.Entities;

public class ElectionCycle
{
    public int Year { get; set; }
    public DateOnly StartOn { get; set; }
    public DateOnly? EndOn { get; set; }

    public ICollection<ElectionCycleVote> Candidates { get; set; }

    public ElectionCycle()
    {
        this.Candidates = Enumerable
            .Empty<ElectionCycleVote>()
            .ToList();
    }
}