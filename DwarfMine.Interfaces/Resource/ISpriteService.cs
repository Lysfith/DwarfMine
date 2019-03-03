using DwarfMine.Models.Util;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Resource
{
    public interface ISpriteService
    {
        Sprite GetSprite(int index);
        AnimatedSprite GetAnimatedSprite(int index);
        void AddOrReplaceSprite(int index, Sprite sprite);
        void AddOrReplaceAnimatedSprite(int index, AnimatedSprite sprite);
        bool SpriteExist(int index);
        bool AnimatedSpriteExist(int index);
    }
}
