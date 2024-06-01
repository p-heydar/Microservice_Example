using MediatR;

namespace BuildingBlocks.CQRS.Command;

public interface ICommandHandler<in TCommand, TResponse>
    :IRequestHandler<TCommand, TResponse>
        where TCommand:ICommand<TResponse>;

public interface ICommandHandler<in TCommand>
    :ICommandHandler<ICommand, Unit>
        where TCommand: ICommand<Unit>;