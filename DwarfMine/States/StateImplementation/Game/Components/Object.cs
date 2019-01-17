using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
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

        public void Update(GameTime time)
        {

        }

        public void Draw(CustomSpriteBatch spriteBatch, GameTime time)
        {
            var sprite = SpriteManager.Instance.GetSprite(EnumSprite.TREE_1);

            spriteBatch.Draw(sprite, new Transform2(X, Y));
        }
    }
}
