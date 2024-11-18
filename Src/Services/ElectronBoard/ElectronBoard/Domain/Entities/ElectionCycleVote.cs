namespace ElectronBoard.Domain.Entities;

public class ElectionCycleVote
{
    public int ElectionCycle { get; set; }
    public long Votes { get; set; }
    public required string FipsCode { get; set; }

    public ElectionCycleVote()
    {
        
    }
}