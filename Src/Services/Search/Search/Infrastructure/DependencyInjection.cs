using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Search.Infrastructure.Configurations;

namespace Search.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection service)
    {
        service.AddScoped(sp =>
        {
            var appSetting = sp.GetRequiredService<IOptions<AppSetting>>().Value.ElasticSearchConfiguration;
            var settings = new ElasticsearchClientSettings(new Uri(appSetting.Host))
                .CertificateFingerprint(appSetting.Fingerprint)
                .Authentication(new BasicAuthentication(appSetting.UserName, appSetting.Password));
    
            var client = new ElasticsearchClient(settings);
            
            return client;
        });

        return service;
    }
}