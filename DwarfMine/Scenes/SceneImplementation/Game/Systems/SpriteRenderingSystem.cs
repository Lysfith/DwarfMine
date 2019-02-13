using DwarfMine.Graphics;
using DwarfMine.States.StateImplementation.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Systems
{
    public class SpriteRenderingSystem : EntityDrawSystem
    {
        private readonly CustomSpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        public SpriteRenderingSystem(CustomSpriteBatch spriteBatch, OrthographicCamera camera)
            : base(Aspect.All(typeof(Transform2)).One(typeof(Sprite), typeof(AnimatedSprite)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            var rectangle = _camera.BoundingRectangle;
            rectangle.Inflate(Constants.TILE_WIDTH * 0.5f, Constants.TILE_HEIGHT * 0.5f);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix(), blendState: BlendState.AlphaBlend);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);

                if (rectangle.Contains(new Point((int)transform.Position.X, (int)transform.Position.Y)))
                {
                    var animatedSprite = _animatedSpriteMapper.Get(entity);
                    var sprite = _spriteMapper.Get(entity);

                    animatedSprite?.Update(gameTime);

                    _spriteBatch.Draw(sprite ?? animatedSprite, transform);
                }
            }

            _spriteBatch.End();
        }
    }
}
