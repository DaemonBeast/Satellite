using Microsoft.AspNetCore.Mvc;
using Satellite.Core.Defaults;
using Satellite.Core.Services;

namespace Satellite.Example.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageSearchController : ControllerBase
{
    private readonly ILogger<PackageSearchController> _logger;
    private readonly IPackageResolverManager _packageResolverManager;

    public PackageSearchController(
        ILogger<PackageSearchController> logger,
        IPackageResolverManager packageResolverManager)
    {
        _logger = logger;
        _packageResolverManager = packageResolverManager;
    }

    [HttpGet("{name}")]
    public IAsyncEnumerable<PackageInfo> Get(string name)
        => _packageResolverManager.FindAsync<SearchCriteria, PackageInfo>(new SearchCriteria(name));
}