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

            SpriteManager.Instance.Load();

#if DEBUG
            DebugGame.SetFont(FontManager.Instance.GetFont("Arial-16"));
#endif

            StateManager.Instance.SetGameState(EnumGameState.Game);
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            DebugGame.StartUpdate();
#endif

            StateManager.Instance.Update(gameTime);

#if DEBUG
            DebugGame.StopUpdate();
#endif

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

#if DEBUG
            DebugGame.StartDraw();
#endif

            StateManager.Instance.Draw(gameTime, GraphicManager.Instance.SpriteBatch);

#if DEBUG
            DebugGame.StopDraw();

            DebugGame.Draw(GraphicManager.Instance.SpriteBatch);
#endif

            base.Draw(gameTime);
        }
    }
}
