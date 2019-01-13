using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Systems
{
    public class RenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Transform2> _transforMapper;

        public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera)
         : base(Aspect.All(typeof(Transform2)).One(typeof(Sprite)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transforMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());

            var rectangle = _camera.BoundingRectangle;

            foreach (var entity in ActiveEntities)
            {
                var transform = _transforMapper.Get(entity);

                var rectangleSprite = new RectangleF(transform.Position.X - Constants.TILE_WIDTH * 0.5f, transform.Position.Y - Constants.TILE_HEIGHT * 0.5f, Constants.TILE_WIDTH, Constants.TILE_HEIGHT); 

                if (rectangle.Intersects(rectangleSprite))
                {
                    var sprite = _spriteMapper.Get(entity);

                    _spriteBatch.Draw(sprite, transform);
                }
            }

            _spriteBatch.End();
        }

       
    }
}
