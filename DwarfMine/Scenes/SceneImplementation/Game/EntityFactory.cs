using Autofac;
using DwarfMine.Core;
using DwarfMine.Interfaces.Resource;
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
            using (var scope = GameCore.Instance.CreateScope())
            {
                var spriteService = scope.Resolve<ISpriteService>();

                var entity = World.CreateEntity();
                entity.Attach(spriteService.GetSprite((int)EnumSprite.CHARACTER_1));
                entity.Attach(new Transform2(x, y));
                entity.Attach(new MovementDestination());
            }
        }

        public void SpawnRock1(float x, float y)
        {
            using (var scope = GameCore.Instance.CreateScope())
            {
                var spriteService = scope.Resolve<ISpriteService>();

                var entity = World.CreateEntity();
                entity.Attach(spriteService.GetSprite((int)EnumSprite.ROCK_1));
                entity.Attach(new Transform2(x, y));
            }
        }

        public void SpawnTree1(float x, float y)
        {
            using (var scope = GameCore.Instance.CreateScope())
            {
                var spriteService = scope.Resolve<ISpriteService>();

                var entity = World.CreateEntity();
                entity.Attach(spriteService.GetSprite((int)EnumSprite.TREE_1));
                entity.Attach(new Transform2(x, y));
            }
        }
    }
}
