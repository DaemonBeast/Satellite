using System.Collections.Immutable;

namespace Satellite.Core;

public interface IPackageInfo
{
    public ImmutableArray<string> DownloadUrls { get; }
}