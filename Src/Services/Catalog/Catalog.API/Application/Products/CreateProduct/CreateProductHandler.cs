using MediatR;

namespace Catalog.API.Application.Products.CreateProduct;

public record CreateProductCommand(string name, List<string> categories, string description, string imageFile, decimal price)
    : IRequest<CreateProductResult>;

public record CreateProductResult(Guid id);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
