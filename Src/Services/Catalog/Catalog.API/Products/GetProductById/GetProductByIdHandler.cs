using BuildingBlocks.CQRS.Query;

using Catalog.API.Exceptions.Products;
using Catalog.API.Models.Catalogs;

using Marten;

namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(Product product);

internal sealed class GetProductByIdHandler(IDocumentSession session) :
    IQueryHandler<GetProductByIdQuery,
        GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var findProductById = await session.Query<Product>()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
                ?? throw new ProductNotFoundException();

        var result = findProductById.Adapt<GetProductByIdResult>();

        return result;
    }
}
