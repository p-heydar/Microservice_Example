using BuildingBlocks.Messaging.Events;
using MassTransit;
using Minio;
using Minio.DataModel.Args;

namespace Media.Catalog.UploadImage.EventHandlers;

public sealed class ProductCreatedEventHandler(IMinioClient _minio):IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        if(context.Message.file is {})
            return;

        var file = context.Message.file;
        
        byte[] decodeBase64 = Convert.FromBase64String(context.Message.file.file);
        var contents = new MemoryStream(decodeBase64);
        
        
        string bucketName = "catalog";
        
        var checkExistBucketCatalog = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

        if (!checkExistBucketCatalog)
            await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

        
        
        var putObject = await _minio
            .PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(file!.fileName)
                .WithContentType(file!.contentType)
                .WithObjectSize(file!.fileSize)
                .WithStreamData(contents));

        string downloadPath = $"{bucketName}/{file.fileName}";

      return;
    }
}