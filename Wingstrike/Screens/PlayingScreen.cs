using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System;

namespace Wingstrike.Screens;

public class PlayingScreen : GameScreen
{
    private new WingstrikeGame Game => (WingstrikeGame) base.Game;

    public PlayingScreen(Game game) : base(game)
    {}

    public override void LoadContent()
    {
        for (int i = 0; i < 1000; i++)
        {
            var ent = Game.CreateEntity();
            var rand = new Random((int)(i * Math.PI));
            var transform = new Transform2(rand.Next(1280), rand.Next(720));
            var rectangle = new ECS.Components.Rectangle(rand.Next(100), rand.Next(100), Color.Red);

            ent.Attach(transform);
            ent.Attach(rectangle);
        }


        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        
    }
}