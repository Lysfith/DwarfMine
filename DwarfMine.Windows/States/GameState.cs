using DwarfMine.Windows.Graphics;
using DwarfMine.Windows.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace DwarfMine.Windows.States
{
    class GameState : IState
    {
        private SpriteFont _font;

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
