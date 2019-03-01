using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Resource
{
    public interface IFontService
    {
        SpriteFont GetFont(string name);
        void AddFont(string name, SpriteFont font);
    }
}
