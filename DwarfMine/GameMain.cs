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

            DebugGame.SetFont(AssetManager.Instance.MainFont);

            SceneManager.Instance.SetScene(EnumScene.Game);
        }

        protected override void Update(GameTime gameTime)
        {
            DebugGame.StartUpdate();

            JobManager.Instance.Update();

            SceneManager.Instance.Update(gameTime, GraphicManager.Instance.Camera);

            DebugGame.StopUpdate();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicManager.Instance.SpriteBatch.ResetCounter();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            DebugGame.StartDraw();

            SceneManager.Instance.Draw(gameTime, GraphicManager.Instance.SpriteBatch, GraphicManager.Instance.Camera);

            DebugGame.StopDraw();

            DebugGame.Draw(GraphicManager.Instance.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
