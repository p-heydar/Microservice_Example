using Carter;
using ElectronBoard.Application.CreateElectionBoards.Dtos;
using ElectronBoard.Domain.Entities;
using ElectronBoard.Infrastructure.SqlServer.Persistence;
using FluentValidation;

namespace ElectronBoard.Application.CreateElectionBoards.Endpoints;

public class CreateElectionBoardEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/ElectionBoard",
            async (ElectionDbContext _dbContext,
                IValidator<CreateElectionBoardRequestDto> validator,
                CreateElectionBoardRequestDto requestDto,
                CancellationToken CancellationToken) =>
            {
                List<ElectionCycleVote> candidateVotes = new();
                
                foreach (var item in requestDto.BoardVoteItems)
                {
                    candidateVotes.Add(new ()
                    {
                        FipsCode = item.FipsCode,
                        ElectionCycle = requestDto.ElectionCycle,
                        Votes = item.Vote
                    });
                }

                await _dbContext.AddRangeAsync(candidateVotes,CancellationToken);
                await _dbContext.SaveChangesAsync(CancellationToken);
                return Results.Created();
            });
    }
}