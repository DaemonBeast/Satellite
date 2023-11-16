using System.Collections.Immutable;

namespace Satellite.Core.Defaults;

public class PackageInfo : IPackageInfo
{
    public string Id { get; }

    public string Name { get; }

    public ImmutableArray<string> DownloadUrls { get; }

    public PackageInfo(string id, string name, IEnumerable<string> downloadUrls)
    {
        Id = id;
        Name = name;
        DownloadUrls = downloadUrls is ImmutableArray<string> d ? d : downloadUrls.ToImmutableArray();
    }
}