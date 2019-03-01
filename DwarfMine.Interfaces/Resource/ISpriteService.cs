using DwarfMine.Models.Util;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Resource
{
    public interface ISpriteService
    {
        Sprite GetSprite(int index);
        void AddOrReplaceSprite(int index, Sprite sprite);
        bool Exist(int index);
    }
}
