using Elasticsearch.Net;
using Nest;
using SalesManagementWebsite.Contracts.Dtos.ElasticSearch;

namespace SalesManagementWebsite.Core.Helpers
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var diseasesIndexName = configuration.GetSection("ElasticSetting:Indices:Item")?.Value;

            IConnectionPool? pool = null;

            if (configuration.GetSection("ElasticSetting:Url").GetChildren().Count() > 0)
            {
                IConfigurationSection[] arrUris = null;
                if (configuration.GetSection("ElasticSetting:Url").GetChildren().Count() > 0)
                    arrUris = configuration.GetSection("v:Url").GetChildren().ToArray();
                else
                    arrUris = new IConfigurationSection[] {
                        configuration.GetSection("Elasticsearch:Url") };
                var uris = arrUris.Select(v => new Uri(v.Value)).ToArray();
                pool = new StaticConnectionPool(uris);
            }
            else
            {
                var uri = new Uri(configuration.GetSection("ElasticSetting:Url").Value);
                pool = new SingleNodeConnectionPool(uri);
            }
            var settings = new ConnectionSettings(pool)
                .DefaultIndex(diseasesIndexName)
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            if (!string.IsNullOrEmpty(configuration.GetSection("ElasticSetting:Username").Value))
            {
                settings.ServerCertificateValidationCallback((o, certificate, chain, errors) => true);
                settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                settings.BasicAuthentication(configuration.GetSection("ElasticSetting:Username").Value,
                                            configuration.GetSection("ElasticSetting:Password").Value);
            }

            settings.DefaultMappingFor<ItemIndex>(o => o.IndexName(configuration.GetSection("ElasticSetting:Indices:Item").Value));

            //TODO handle later for authentication elasticsearch
            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
