using Microsoft.Extensions.DependencyInjection;

namespace Wingstrike.Engine.Startup;

public interface IStartupProvider
{
    void ConfigureService(IServiceCollection services);
}