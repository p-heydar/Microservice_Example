using System.Net;
using Minio;

namespace Media;

public static class DependencyInjection
{
    public static IServiceCollection AddMinIoClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(sp =>
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    
            var endpoint = configuration["MinIo:Endpoint"];
            var accessKey = configuration["MinIo:Credentials"];
            var secretKey = configuration["MinIo:SecretKey"];
        
            var minio = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(true)
                .Build();
            
            return minio;    
        });
        
        
        return services;
    }
}