using BuildingBlocks.CQRS.Command;

using Catalog.API.Exceptions.Products;
using Catalog.API.Models.Catalogs;

using Marten;

using System.Windows.Input;

namespace Catalog.API.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;

public sealed record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var findProductById = await session.Query<Product>()
            .FirstOrDefaultAsync(x => x.Id == command.Id)
                ?? throw new ProductNotFoundException(command.Id);

            session.Delete(findProductById);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
         
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        finally
        {
            await session.DisposeAsync();
        }
    }
}
