
using DwarfMine.Scenes.SceneImplementation.Game.Components;
using DwarfMine.States.StateImplementation.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Systems
{
    public class SpriteVisibilitySystem : EntityUpdateSystem
    {
        private readonly OrthographicCamera _camera;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Visibility> _visibilityMapper;

        public SpriteVisibilitySystem(OrthographicCamera camera)
            : base(Aspect.All(typeof(Transform2)))
        {
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _visibilityMapper = mapperService.GetMapper<Visibility>();
        }

        public override void Update(GameTime gameTime)
        {
            var rectangle = _camera.BoundingRectangle;
            rectangle.Inflate(Constants.TILE_WIDTH * 0.5f, Constants.TILE_HEIGHT * 0.5f);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var visibility = _visibilityMapper.Get(entity);

                if (rectangle.Contains(new Point((int)transform.Position.X, (int)transform.Position.Y)))
                {
                    if (visibility == null)
                    {
                        _visibilityMapper.Put(entity, new Visibility());
                    }
                }
                else if(visibility != null)
                {
                    _visibilityMapper.Delete(entity);
                }
            }
        }
    }
}
