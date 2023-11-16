using Satellite.Core.Defaults;
using Satellite.Core.Extensions;
using Satellite.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHttpClient()
    .AddSatellite()
    .AddPackageResolver<SearchCriteria, PackageInfo, NaiveExamplePackageResolver>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();