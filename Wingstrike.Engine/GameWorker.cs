using Microsoft.Extensions.Hosting;

namespace Wingstrike.Engine;

public class GameWorker : IHostedService
{
    private readonly IGame _game;
    private readonly IHostApplicationLifetime _lifetime;

    public GameWorker(IGame game, IHostApplicationLifetime lifetime)
    {
        _game = game;
        _lifetime = lifetime;
    }

    public Task StartAsync(CancellationToken ct)
    {
        _lifetime.ApplicationStarted.Register(OnStarted);
        _lifetime.ApplicationStopping.Register(OnStopping);
        _lifetime.ApplicationStopped.Register(OnStopped);

        _game.Exiting += OnGameExiting;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct)
    {
        _lifetime.StopApplication();

        return Task.CompletedTask;
    }

    private void OnGameExiting(object? sender, EventArgs args)
    {
        StopAsync(new CancellationToken());
    }

    private void OnStarted()
    {
        _game.Run();
    }

    private void OnStopping()
    {
    }

    private void OnStopped()
    {
    }
}