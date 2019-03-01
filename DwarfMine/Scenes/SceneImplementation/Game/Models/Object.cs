

using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Resource;
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
            using (var scope = GameCore.Instance.CreateScope())
            {
                var spriteService = scope.Resolve<ISpriteService>();

                var sprite = spriteService.GetSprite((int)Sprite);

                spriteBatch.Draw(sprite, new Transform2(X, Y));
            }

            
        }
    }
}
