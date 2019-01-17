using DwarfMine.Graphics;
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
        private class Entity
        {
            public int Id { get; set; }
            public Transform2 Transform { get; set; }
            public Sprite Sprite { get; set; }
        }

        private readonly CustomSpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Transform2> _transforMapper;

        public RenderSystem(CustomSpriteBatch spriteBatch, OrthographicCamera camera)
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

            var entities = ActiveEntities.Select(x => new Entity()
            {
                Id = x,
                Transform = _transforMapper.Get(x)
            })
            .Where(x => rectangle.Contains(x.Transform.Position)).ToList();

            entities.Sort(delegate (Entity a, Entity b)
            {
                if (a.Transform.Position.Y == b.Transform.Position.Y)
                {
                    return a.Transform.Position.X < b.Transform.Position.X ? -1 : 1;
                }
                return a.Transform.Position.Y < b.Transform.Position.Y ? -1 : 1;
            });

            foreach (var entity in entities)
            {
                var sprite = _spriteMapper.Get(entity.Id);

                _spriteBatch.Draw(sprite, entity.Transform);
            }

            _spriteBatch.End();
        }

       
    }
}
