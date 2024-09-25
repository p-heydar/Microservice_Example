using Carter;
using Minio;
using Minio.DataModel.Args;

namespace Media.Application.Catalog.UploadImage;

public sealed class UploadImageEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Catalog/UploadImage", async (IMinioClient _minioClient, IFormFile file) =>
        {
            var bucketName = "catalog";
            var checkExistBucketCatalog = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

           if (!checkExistBucketCatalog)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

           var putObject = await _minioClient
               .PutObjectAsync(new PutObjectArgs()
                   .WithBucket(bucketName)
                   .WithObject(file.FileName)
                   .WithContentType(file.ContentType)
                   .WithObjectSize(file.Length)
                   .WithStreamData(file.OpenReadStream()));

           var memoryStream = new MemoryStream();

           GetObjectArgs getObjectArgs = new GetObjectArgs()
               .WithObject(file.FileName)
               .WithBucket(bucketName)
               .WithCallbackStream((stream) =>
               {
                    stream.CopyTo(memoryStream);
               });
           
           return Results.File(memoryStream);
           // PutObjectArgs args = new PutObjectArgs().WithBucket("Catalog")
           // _minioClient.PutObjectAsync()
        }).DisableAntiforgery();
    }
}