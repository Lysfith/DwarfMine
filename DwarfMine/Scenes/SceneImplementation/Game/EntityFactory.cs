﻿using DwarfMine.Managers;
using DwarfMine.Scenes.SceneImplementation.Game.Components;
using DwarfMine.States.StateImplementation.Game;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game
{
    public class EntityFactory
    {
        public World World { get; private set; }

        public EntityFactory(World world)
        {
            World = world;
        }

        public void SpawnPlayer(float x, float y)
        {
            var entity = World.CreateEntity();
            entity.Attach(SpriteManager.Instance.GetSprite(EnumSprite.DEV));
            entity.Attach(new Transform2(x, y));
            entity.Attach(new MovementDestination());
        }
    }
}