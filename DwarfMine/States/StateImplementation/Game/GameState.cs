using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
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
        private TiledMap _map;
        private TiledMapRenderer _renderer;
        private OrthographicCamera _camera;

        public GameState()
        {
            _font = FontManager.Instance.GetFont("Arial-16");
        }

        public void Start()
        {

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

        public void Update(double time)
        {

        }

        public void Draw(CustomSpriteBatch spritebatch)
        {
            spritebatch.Begin();


            spritebatch.End();
        }
    }
}
