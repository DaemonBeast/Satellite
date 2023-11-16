using Microsoft.Extensions.DependencyInjection;
using Satellite.Core.Services;

namespace Satellite.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSatellite(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IPackageResolverManager), typeof(PackageResolverManager));
        return services;
    }

    public static IServiceCollection AddPackageResolver<TCriteria, TPackageInfo, TPackageResolver>(
        this IServiceCollection services)
        where TPackageInfo : class, IPackageInfo
        where TPackageResolver : class, IPackageResolver<TCriteria, TPackageInfo>
    {
        services.AddSingleton<IPackageResolver, TPackageResolver>();
        return services;
    }
}