#region Using Statements
using System;
using DwarfMine.Windows.Managers;
using DwarfMine.Windows.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DwarfMine.Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DwarfMineGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;		

        public DwarfMineGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);     
            graphics.IsFullScreen = false;		
            graphics.PreferredBackBufferWidth = 1920;		
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            IsFixedTimeStep = true;
            IsMouseVisible = true;

            TargetElapsedTime = TimeSpan.FromMilliseconds(16.7);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GraphicManager.Instance.SetGraphicsDevice(GraphicsDevice);

            FontManager.Instance.AddFont("Arial-10", Content.Load<SpriteFont>("Fonts /Arial-10"));
            FontManager.Instance.AddFont("Arial-16", Content.Load<SpriteFont>("Fonts /Arial-16"));
            FontManager.Instance.AddFont("Arial-24", Content.Load<SpriteFont>("Fonts /Arial-24"));
            FontManager.Instance.AddFont("Arial-36", Content.Load<SpriteFont>("Fonts /Arial-36"));

            TextureManager.Instance.SetContentManager(Content);

            StateManager.Instance.SetGameState(EnumGameState.Game);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            DebugGame.StartUpdate();

            // For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
            #if !__IOS__ &&  !__TVOS__
            if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit ();
            }
#endif

            StateManager.Instance.Update(gameTime.ElapsedGameTime.TotalSeconds);

            DebugGame.StopUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
