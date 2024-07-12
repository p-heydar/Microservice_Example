using BuildingBlocks.CQRS.Query;

using Catalog.API.Exceptions.Products;
using Catalog.API.Models.Catalogs;

using Marten;

namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public  record GetProductByIdResult(Product Product);

internal sealed class GetProductByIdHandler(IDocumentSession session) :
    IQueryHandler<GetProductByIdQuery,
        GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var findProductById = await session.Query<Product>()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);


        return new GetProductByIdResult(findProductById);
    }
}
