﻿using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game
{
    public class EntityFactory
    {
        private readonly World _world;

        public EntityFactory(World world)
        {
            _world = world;
        }

        public Entity CreateTile(Vector2 position)
        {
            string spriteName = "Textures/Game/tiles";
            Sprite sprite = null;

            if(!SpriteManager.Instance.Exist(spriteName))
            {
                var tileTexture = TextureManager.Instance.GetTexture(spriteName);
                sprite = new Sprite(tileTexture);

                SpriteManager.Instance.AddOrReplaceSprite(spriteName, sprite);
            }
            else
            {
                sprite = SpriteManager.Instance.GetSprite(spriteName);
            }

            var entity = _world.CreateEntity();
            entity.Attach(new Transform2(position, 0, Vector2.One));
            entity.Attach(sprite);
            //entity.Attach(new Body { Position = position, Size = new Vector2(32, 64), BodyType = BodyType.Dynamic });
            entity.Attach(new Player());
            return entity;
        }
    }
}