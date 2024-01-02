using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Screens;
using Wingstrike.ECS.Systems;
using Wingstrike.Engine;
using Wingstrike.Screens;

namespace Wingstrike;

public class WingstrikeGame : Game, IGame
{
    private readonly ScreenManager _screenManager;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World _world;

    public Game Game => this;
    public SpriteBatch SpriteBatch => _spriteBatch;

    public WingstrikeGame(ScreenManager screenManager)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _screenManager = screenManager;
        Components.Add(_screenManager);
    }

    public Entity CreateEntity() => _world.CreateEntity();

    public void DestroyEntity(Entity entity) => _world.DestroyEntity(entity);

    public void DestroyEntity(int entityId) => _world.DestroyEntity(entityId);

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        base.Initialize();
        _screenManager.LoadScreen(new PlayingScreen(this));
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _world = new WorldBuilder()
            .AddSystem(new RenderSystem(_spriteBatch))
            .AddSystem(new TestSystem())
            .Build();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _world.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _world.Draw(gameTime);
        base.Draw(gameTime);
    }
}