namespace Satellite.Core;

public readonly record struct ResolvedPackages(IEnumerable<IPackageInfo> Packages)
{
    public IEnumerable<TPackageInfo>? Upcast<TPackageInfo>()
        where TPackageInfo : class, IPackageInfo
        => Packages as IEnumerable<TPackageInfo>;
}