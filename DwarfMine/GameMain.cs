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
        public GameMain()
        {
        }

        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            GraphicManager.Instance.SetGraphicsDevice(GraphicsDevice);
            GraphicManager.Instance.SetCamera(new OrthographicCamera(GraphicsDevice));
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

            StateManager.Instance.Update(gameTime);

            DebugGame.StopUpdate();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DebugGame.StartDraw();

            StateManager.Instance.Draw(gameTime, GraphicManager.Instance.SpriteBatch);

            DebugGame.StopDraw();

            DebugGame.Draw(GraphicManager.Instance.SpriteBatch, FontManager.Instance.GetFont("Arial-16"));

            base.Draw(gameTime);
        }
    }
}
