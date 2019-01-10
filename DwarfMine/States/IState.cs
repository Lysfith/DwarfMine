using DwarfMine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States
{
    public interface IState
    {
        void Start();
        void End();
        void Pause();
        void Resume();
        void Update(GameTime time);
        void Draw(GameTime time, CustomSpriteBatch spritebatch);
    }
}
