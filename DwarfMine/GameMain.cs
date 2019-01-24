using Autofac;
using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States;
using DwarfMine.Utils;
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
            FontManager.Instance.Load(Content);
            TextureManager.Instance.Load(Content);
            SpriteManager.Instance.Load(Content);
            AssetManager.Instance.Load(Content);

#if DEBUG
            DebugGame.SetFont(AssetManager.Instance.MainFont);
#endif

            SceneManager.Instance.SetScene(EnumScene.Game);
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            DebugGame.StartUpdate();
#endif

            SceneManager.Instance.Update(gameTime, GraphicManager.Instance.Camera);

#if DEBUG
            DebugGame.StopUpdate();
#endif

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicManager.Instance.SpriteBatch.ResetCounter();

            GraphicsDevice.Clear(Color.CornflowerBlue);

#if DEBUG
            DebugGame.StartDraw();
#endif

            SceneManager.Instance.Draw(gameTime, GraphicManager.Instance.SpriteBatch, GraphicManager.Instance.Camera);

#if DEBUG
            DebugGame.StopDraw();

            DebugGame.Draw(GraphicManager.Instance.SpriteBatch);
#endif

            base.Draw(gameTime);
        }
    }
}
