using FluentValidation;

namespace ElectronBoard.Application.CreateElectionBoards.Dtos;

public sealed record CreateElectionBoardRequestDto(int ElectionCycle, List<CreateElectionBoardVoteItem> BoardVoteItems);

public sealed record CreateElectionBoardVoteItem(long Vote, string FipsCode);


public sealed class CreateElectionBoardRequestDtoValidator:AbstractValidator<CreateElectionBoardRequestDto>
{
    public CreateElectionBoardRequestDtoValidator()
    {
        RuleFor(x => x.ElectionCycle)
            .GreaterThan(2000)
                .WithMessage("The election year is incorrect and must be greater than 2000.")
            .Must(x => (x % 4) == 0)
                .WithMessage("The electoral cycle vote are incorrect.");
    }
}