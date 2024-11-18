using System.Text.Json;
using Carter;
using Confluent.Kafka;
using ElectronBoard.Consuming.VoteAssigment;

namespace ElectronBoard.Application.AddVotes.Endpoints;

public class AddVoteEndpoint:ICarterModule
{
    private const string _topic = "us-vote-assignment";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("AddVote", async () =>
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:19092",
                AllowAutoCreateTopics = true,
                EnableIdempotence = true
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();
            var message = new VoteAssignmentEvent()
            {
                FipsCode = "01",
                CandidateCode = "17882"
            };

         var res=    await producer.ProduceAsync(_topic, new Message<Null, string>()
            {
                Value = JsonSerializer.Serialize(message)
            });
            
        });
    }
}