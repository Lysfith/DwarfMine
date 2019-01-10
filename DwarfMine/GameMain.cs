using Autofac;
using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine
{
    public class GameMain : GameBase
    {
        private OrthographicCamera _camera;
        private CustomSpriteBatch _spriteBatch;

        public GameMain()
        {
        }

        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            _camera = new OrthographicCamera(GraphicsDevice);

            _spriteBatch = new CustomSpriteBatch(GraphicsDevice);

            GraphicManager.Instance.SetGraphicsDevice(GraphicsDevice);

            builder.RegisterInstance(_spriteBatch);
            builder.RegisterInstance(_camera);
        }

        protected override void LoadContent()
        {
            FontManager.Instance.AddFont("Arial-10", Content.Load<SpriteFont>("Fonts/Arial-10"));
            FontManager.Instance.AddFont("Arial-16", Content.Load<SpriteFont>("Fonts/Arial-16"));
            FontManager.Instance.AddFont("Arial-24", Content.Load<SpriteFont>("Fonts/Arial-24"));
            FontManager.Instance.AddFont("Arial-36", Content.Load<SpriteFont>("Fonts/Arial-36"));

            TextureManager.Instance.SetContentManager(Content);

            StateManager.Instance.SetGameState(EnumGameState.Game);
        }

        protected override void Update(GameTime gameTime)
        {
            DebugGame.StartUpdate();

            StateManager.Instance.Update(gameTime.ElapsedGameTime.TotalSeconds);

            DebugGame.StopUpdate();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DebugGame.StartDraw();

            StateManager.Instance.Draw(GraphicManager.Instance.SpriteBatch);

            DebugGame.StopDraw();

            DebugGame.Draw(GraphicManager.Instance.SpriteBatch, FontManager.Instance.GetFont("Arial-16"));

            base.Draw(gameTime);
        }
    }
}
