using Microsoft.Extensions.DependencyInjection;
using MonoGame.Extended.Screens;
using System.ComponentModel.Composition;
using Wingstrike.Engine.Startup;

namespace Wingstrike.Startup;

[Export(typeof(IStartupProvider))]
public class WingstrikeStartupProvider : IStartupProvider
{
    public void ConfigureService(IServiceCollection services)
    {
        services.AddSingleton<ScreenManager>();
    }
}