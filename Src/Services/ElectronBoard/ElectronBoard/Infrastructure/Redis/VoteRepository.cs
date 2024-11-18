using StackExchange.Redis;

namespace ElectronBoard.Infrastructure.Redis;

public class VoteRepository(IConnectionMultiplexer connectionMultiplexer)
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task VoteAssignment(string candidateCode, string fipsCode)
    {
        var sortedSetKey = $"election:state{fipsCode}";
        await _database.SortedSetIncrementAsync(
            key: sortedSetKey,
            member: candidateCode,
            value: 1);
    }
}