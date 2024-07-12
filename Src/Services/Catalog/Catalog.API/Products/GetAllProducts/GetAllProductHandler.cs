using System.Collections.Immutable;
using BuildingBlocks.CQRS.Query;
using Catalog.API.Models.Catalogs;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetAllProducts;

public sealed record GetAllProductQuery(short pageNumber, int pageSize):IQuery<GetAllProductResult>;

public sealed record GetAllProductResult(IEnumerable<Product> Products);

internal sealed class GetAllProductHandler(IDocumentSession session) : IQueryHandler<GetAllProductQuery, GetAllProductResult>
{
    public async Task<GetAllProductResult> Handle(GetAllProductQuery query,
        CancellationToken cancellationToken)
    {
        var getAllProduct = await session
            .Query<Product>().Where(x=>x.Price>0).ToPagedListAsync(query.pageNumber, query.pageSize);
        
        return new GetAllProductResult(getAllProduct);
    }
}
