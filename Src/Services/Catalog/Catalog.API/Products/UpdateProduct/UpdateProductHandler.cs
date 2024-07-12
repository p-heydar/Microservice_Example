using BuildingBlocks.CQRS.Command;

using Catalog.API.Exceptions.Products;
using Catalog.API.Models.Catalogs;

using Marten;
using Marten.Linq.SoftDeletes;

namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;


public sealed record UpdateProductResult(bool IsSuccess);


internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
       
            if ((command.Product.Id.Equals(Guid.Empty)))
                throw new EmptyGuidException();

            var findProductById = await session.Query<Product>()
                .FirstOrDefaultAsync(product => product.Id == command.Product.Id)
                    ?? throw new ProductNotFoundException(command.Product.Id);

            findProductById.Name = command.Product.Name!;
            findProductById.Description = command.Product.Description!;
            findProductById.ImageFile = command.Product.ImageFile!;
            findProductById.Price = command.Product.Price!;
            findProductById.Categories = command.Product.Categories!;

            session.Update(findProductById!);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);


       
    }
}
