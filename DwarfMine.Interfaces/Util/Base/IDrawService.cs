using DwarfMine.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Util.Base
{
    public interface IDrawService
    {
        void Draw(GameTime gameTime, CustomSpriteBatch spriteBatch);
    }
}
