using System.Collections.Immutable;
using BuildingBlocks.CQRS.Query;
using Catalog.API.Models.Catalogs;
using Marten;

namespace Catalog.API.Products.GetAllProducts;

public sealed record GetAllProductQuery():IQuery<GetAllProductResult>;

public sealed record GetAllProductResult(IEnumerable<Product> Products);

internal sealed class GetAllProductHandler(IDocumentSession session) : IQueryHandler<GetAllProductQuery, GetAllProductResult>
{
    public async Task<GetAllProductResult> Handle(GetAllProductQuery query,
        CancellationToken cancellationToken)
    {
        var getAllProduct = session
            .Query<Product>()
            .ToImmutableList();

        return new GetAllProductResult(getAllProduct);
    }
}
