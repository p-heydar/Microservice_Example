namespace ElectronBoard.Domain.Entities;

public class State
{
    public required string Name { get; set; }
    public required string FipsCode { get; set; }
    public required int ElectralVotes { get; set; }

    public State()
    {
        
    }
}