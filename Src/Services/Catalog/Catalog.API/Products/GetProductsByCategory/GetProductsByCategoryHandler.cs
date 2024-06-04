using BuildingBlocks.CQRS.Query;

using Catalog.API.Exceptions.Products;
using Catalog.API.Models.Catalogs;

using Marten;

using System.Collections.Immutable;

namespace Catalog.API.Products.GetProductsByCategory;


public sealed record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public sealed record GetProductsByCategoryResult(IEnumerable<Product> Products);


internal sealed class GetProductsByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery,
    GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var findProductsByCategory = session.Query<Product>()
        .Where(product => product.Categories
            .Contains(query.Category))
                .ToImmutableList()
                    ?? throw new ProductNotFoundException();

        var result = new GetProductsByCategoryResult(findProductsByCategory);

        return result;
    }
}
