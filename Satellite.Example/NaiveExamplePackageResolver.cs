using System.Text.Json.Serialization;
using Satellite.Core;
using Satellite.Core.Defaults;

namespace Satellite.Example;

public class NaiveExamplePackageResolver : IPackageResolver<SearchCriteria, PackageInfo>
{
    private readonly IHttpClientFactory _httpClientFactory;

    private const string PackagesUrl =
        "https://raw.githubusercontent.com/DaemonBeast/Satellite.Example.Packages/master/LooksLikeAPackageDatabaseToMe.json";

    public NaiveExamplePackageResolver(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async ValueTask<ResolvedPackages> ResolveAsync(SearchCriteria criteria)
    {
        var httpClient = _httpClientFactory.CreateClient();

        if (await httpClient.GetFromJsonAsync(PackagesUrl, typeof(DatabasePackageSchema[]))
            is not DatabasePackageSchema[] packages)
        {
            return new ResolvedPackages(Enumerable.Empty<IPackageInfo>());
        }

        return new ResolvedPackages(
            packages
                .Where(package => package.Name.Contains(criteria.Query, StringComparison.InvariantCultureIgnoreCase) ||
                                  package.Id.Contains(criteria.Query, StringComparison.InvariantCultureIgnoreCase))
                .Select(package => new PackageInfo(package.Id, package.Name, new[] { package.DownloadUrl })));
    }
}

public class DatabasePackageSchema
{
    [JsonPropertyName("id")]
    public string Id { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; }

    public DatabasePackageSchema(string id, string name, string downloadUrl)
    {
        Id = id;
        Name = name;
        DownloadUrl = downloadUrl;
    }
}