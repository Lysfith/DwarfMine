using DwarfMine.Models.Util;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Resource
{
    public interface ITextureService
    {
        Texture2D GetTexture(string name);
        void AddTexture(string name, Texture2D texture);
    }
}
