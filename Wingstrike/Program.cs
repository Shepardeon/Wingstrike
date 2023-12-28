using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wingstrike;
using Wingstrike.Engine;
using Wingstrike.Engine.Extensions;

CreateHostBuilder()
    .Build()
    .Run();

static IHostBuilder CreateHostBuilder()
    => Host.CreateDefaultBuilder()
        .ConfigureServices((ctx, services) =>
        {
            services
                .AddHostedService<GameWorker>()
                .AddGameConfiguration<WingstrikeGame>()
                .AddPlugins();
        });