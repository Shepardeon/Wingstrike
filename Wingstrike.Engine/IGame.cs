using Microsoft.Xna.Framework;

namespace Wingstrike.Engine;

public interface IGame : IDisposable
{
    Game Game { get; }

    GameWindow Window { get; }

    event EventHandler<EventArgs> Exiting;

    void Run();
    void Exit();
}
