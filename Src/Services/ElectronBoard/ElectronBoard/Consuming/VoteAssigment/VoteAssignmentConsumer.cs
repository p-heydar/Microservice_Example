using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using ElectronBoard.Infrastructure.Redis;
using Mapster;

namespace ElectronBoard.Consuming.VoteAssigment;

public class VoteAssignmentConsumer:BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private const string _topic = "us-vote-assignment";
    private readonly VoteRepository _voteRepository;
    
    public VoteAssignmentConsumer(VoteRepository voteRepository)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:19092",
            GroupId = "us-assignment-election-board",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            IsolationLevel = IsolationLevel.ReadCommitted,
            EnableAutoCommit = false
        };
        _voteRepository = voteRepository;
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        _consumer.Subscribe(_topic);
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = _consumer.Consume(stoppingToken);
            var assignment = JsonSerializer.Deserialize<VoteAssignmentEvent>(message.Message.Value);

            await _voteRepository.VoteAssignment(assignment.CandidateCode, assignment.FipsCode);
            _consumer.Commit();
        }
    }
}