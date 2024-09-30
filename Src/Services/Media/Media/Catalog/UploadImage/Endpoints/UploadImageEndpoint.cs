using Carter;
using Minio;
using Minio.DataModel.Args;

namespace Media.Catalog.UploadImage.Endpoints;

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

           string downloadPath = $"{bucketName}/{file.FileName}";

           return Results.Ok(downloadPath);
        }).DisableAntiforgery();
    }
}