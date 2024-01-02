using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Wingstrike.ECS.Systems;

public class RenderSystem : EntityDrawSystem
{
    private readonly SpriteBatch _spriteBatch;
    private ComponentMapper<Transform2> _transformMapper;
    private ComponentMapper<Components.Rectangle> _rectangleMapper;

    public RenderSystem(SpriteBatch spriteBatch) : base(Aspect.All(typeof(Transform2), typeof(Components.Rectangle)))
    {
        _spriteBatch = spriteBatch;
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();

        foreach (var entId in ActiveEntities)
        {
            var transform = _transformMapper.Get(entId);
            var rectangle = _rectangleMapper.Get(entId);

            _spriteBatch.DrawRectangle(transform.Position.X, transform.Position.Y, rectangle.Width, rectangle.Height, rectangle.Color);
        }

        _spriteBatch.End();
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _rectangleMapper = mapperService.GetMapper<Components.Rectangle>();
    }
}