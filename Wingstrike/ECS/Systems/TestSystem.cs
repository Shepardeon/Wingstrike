using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace Wingstrike.ECS.Systems;

public class TestSystem : EntityUpdateSystem
{
    private ComponentMapper<Transform2> _transformMapper;

    public TestSystem() : base(Aspect.All(typeof(Transform2)))
    {}

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Update(GameTime gameTime)
    {
        var dt = gameTime.GetElapsedSeconds();
        var rand = new Random();

        foreach (var entId in ActiveEntities)
        {
            var transform = _transformMapper.Get(entId);

            transform.Position = new Vector2((transform.Position.X + rand.Next(300) * dt)%1280,
                transform.Position.Y);
        }
    }
}