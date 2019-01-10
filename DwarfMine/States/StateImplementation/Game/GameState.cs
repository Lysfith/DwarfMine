using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace DwarfMine.States.StateImplementation.Game
{
    class GameState : IState
    {
        private SpriteFont _font;
        private World _world;
        private EntityFactory _entityFactory;

        public GameState()
        {
            _font = FontManager.Instance.GetFont("Arial-16");
        }

        public void Start()
        {
            _world = new WorldBuilder()
               //.AddSystem(new WorldSystem())
               //.AddSystem(new PlayerSystem())
               //.AddSystem(new EnemySystem())
               .AddSystem(new RenderSystem(GraphicManager.Instance.SpriteBatch, GraphicManager.Instance.Camera))
               .Build();

            _entityFactory = new EntityFactory(_world);

            var camera = GraphicManager.Instance.Camera;

            camera.LookAt(Vector2.Zero);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _entityFactory.CreateTile(new Vector2(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT));
                }
            }
        }

        public void End()
        {

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void Update(GameTime time)
        {
            //_renderer.Update(time);

            _world.Update(time);
        }

        public void Draw(GameTime time, CustomSpriteBatch spritebatch)
        {
            //_renderer.Draw(_camera.GetViewMatrix());

            _world.Draw(time);
        }
    }
}
