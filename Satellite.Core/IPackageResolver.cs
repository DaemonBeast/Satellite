namespace Satellite.Core;

public interface IPackageResolver<in TCriteria, out TPackageInfo> : IPackageResolver
    where TPackageInfo : class, IPackageInfo
{
    public ValueTask<ResolvedPackages> ResolveAsync(TCriteria criteria);
}

public interface IPackageResolver
{
}