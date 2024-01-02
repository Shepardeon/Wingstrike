using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Text;
using Wingstrike.Engine.Startup;

namespace Wingstrike.Engine.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameConfiguration<TGame>(this IServiceCollection services) where TGame : class, IGame
    {
        // Base DI
        return services.AddSingleton<IGame, TGame>();
    }

    public static IServiceCollection AddPlugins(this IServiceCollection services)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var dirCat = new DirectoryCatalog(path);
        var importDef = BuildImportDefinition();

        try
        {
            using var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(dirCat);

            using var compositionContainer = new CompositionContainer(aggregateCatalog);
            var exports = compositionContainer.GetExports(importDef);

            foreach (var module in exports.Select(x => x.Value as IStartupProvider).Where(x => x != null))
            {
                module?.ConfigureService(services);
            }
        }
        catch (ReflectionTypeLoadException e)
        {
            var builder = new StringBuilder();
            foreach (var loaderException in e.LoaderExceptions)
            {
                builder.AppendFormat("{0}\n", loaderException?.Message);
            }

            throw new TypeLoadException(builder.ToString(), e);
        }

        return services;
    }

    private static ImportDefinition BuildImportDefinition()
        => new ImportDefinition(x => true, typeof(IStartupProvider).FullName, ImportCardinality.ZeroOrMore, false, false);
}
