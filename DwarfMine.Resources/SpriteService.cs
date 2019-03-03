using DwarfMine.Interfaces.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Resources
{
    public class SpriteService : ISpriteService
    {
        private Dictionary<int, Sprite> _sprites;
        private Dictionary<int, AnimatedSprite> _animatedSprites;

        public SpriteService()
        {
            _sprites = new Dictionary<int, Sprite>();
            _animatedSprites = new Dictionary<int, AnimatedSprite>();
        }

        public bool SpriteExist(int index)
        {
            return _sprites.ContainsKey(index);
        }

        public bool AnimatedSpriteExist(int index)
        {
            return _animatedSprites.ContainsKey(index);
        }

        public Sprite GetSprite(int index)
        {
            if (SpriteExist(index))
            {
                return _sprites[index];
            }

            throw new Exception("Le sprite " + index + " n'existe pas");
        }

        public AnimatedSprite GetAnimatedSprite(int index)
        {
            if (AnimatedSpriteExist(index))
            {
                return _animatedSprites[index];
            }

            throw new Exception("Le sprite " + index + " n'existe pas");
        }

        public void AddOrReplaceSprite(int index, Sprite sprite)
        {
            if (SpriteExist(index))
            {
                _sprites[index] = sprite;
            }
            else
            {
                _sprites.Add(index, sprite);
            }
        }

        public void AddOrReplaceAnimatedSprite(int index, AnimatedSprite sprite)
        {
            if (AnimatedSpriteExist(index))
            {
                _animatedSprites[index] = sprite;
            }
            else
            {
                _animatedSprites.Add(index, sprite);
            }
        }
    }
}
