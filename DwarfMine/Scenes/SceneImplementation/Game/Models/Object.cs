using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Models
{
    public class Object
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public EnumSprite Sprite { get; protected set; }

        public Object(int x, int y, EnumSprite sprite)
        {
            X = x;
            Y = y;
            Sprite = sprite;
        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(CustomSpriteBatch spriteBatch, GameTime time)
        {
            var sprite = SpriteManager.Instance.GetSprite(Sprite);

            spriteBatch.Draw(sprite, new Transform2(X, Y));
        }
    }
}
