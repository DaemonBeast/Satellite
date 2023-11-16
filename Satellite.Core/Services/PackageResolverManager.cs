using System.Collections.Immutable;

namespace Satellite.Core.Services;

public class PackageResolverManager : IPackageResolverManager
{
    public ImmutableArray<IPackageResolver> Resolvers { get; }

    public PackageResolverManager(IEnumerable<IPackageResolver> resolvers)
    {
        Resolvers = resolvers.ToImmutableArray();
    }

    public async IAsyncEnumerable<TPackageInfo> FindAsync<TCriteria, TPackageInfo>(TCriteria criteria)
        where TPackageInfo : class, IPackageInfo
    {
        var resolvers = Resolvers
            .Select(resolver => resolver as IPackageResolver<TCriteria, TPackageInfo>)
            .Where(resolver => resolver != null)
            !.Select<IPackageResolver<TCriteria, TPackageInfo>, ValueTask<ResolvedPackages>>(
                resolver => resolver!.ResolveAsync(criteria))
            .ToArray();

        foreach (var resolver in resolvers)
        {
            foreach (var package in (await resolver).Upcast<TPackageInfo>()!)
            {
                yield return package;
            }
        }
    }
}

public interface IPackageResolverManager
{
    public ImmutableArray<IPackageResolver> Resolvers { get; }

    public IAsyncEnumerable<TPackageInfo> FindAsync<TCriteria, TPackageInfo>(TCriteria criteria)
        where TPackageInfo : class, IPackageInfo;
}