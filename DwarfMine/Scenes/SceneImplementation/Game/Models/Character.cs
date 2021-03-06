﻿

using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Resource;
using DwarfMine.Interfaces.Util;
using DwarfMine.Scenes.SceneImplementation.Game.Systems;
using DwarfMine.States.StateImplementation.Game.Layers.RegionLayers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Models
{
    public class Character : Object
    {
        public Point? Destination { get; protected set; }
        public Vector2 Position { get; protected set; }

        public Queue<Point> _path;

        public Character(int x, int y)
            : base(x, y, EnumSprite.DEV)
        {
            Position = new Vector2(X, Y);
        }

        public override void Update(GameTime time)
        {
            if (_path != null && _path.Any())
            {
                var nextDestination = _path.Peek();

                if ((nextDestination.ToVector2() - Position).Length() < 1f)
                {
                    _path.Dequeue();
                }
                else
                {
                    var direction = (nextDestination.ToVector2() - Position);

                    direction /= direction.Length();

                    var nextPosition = Position + direction * (float)time.ElapsedGameTime.TotalSeconds * 100f;

                    Position = nextPosition;

                    X = (int)Position.X;
                    Y = (int)Position.Y;
                }
            }
        }

        public override void Draw(CustomSpriteBatch spriteBatch, GameTime time)
        {
            if (_path != null && _path.Any())
            {
                Sprite sprite = null;

                using (var scope = GameCore.Instance.CreateScope())
                {
                    var spriteService = scope.Resolve<ISpriteService>();

                    sprite = spriteService.GetSprite((int)EnumSprite.DEV2);
                }

                foreach (var p in _path)
                {
                    spriteBatch.Draw(sprite, p.ToVector2());
                }
            }

            base.Draw(spriteBatch, time);
        }

        public void SetDestination(Point? destination)
        {
            var region = MapSystem.Instance.GetRegion(destination.Value.X, destination.Value.Y);
            var positionInWorld = region.GetCellPositionFromWorldPosition(destination.Value.X, destination.Value.Y);

            Destination = positionInWorld;

            CreatePath();
        }

        private void CreatePath()
        {
            _path = new Queue<Point>();

            var region = MapSystem.Instance.GetRegion(X, Y);

            var costs = region.GetCosts();

            var destinationInRegion = region.GetCellIndexFromWorldPosition(Destination.Value.X, Destination.Value.Y);

            Vector2[,] flows = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var flowFieldService = scope.Resolve<IFlowFieldService>();

                flows = flowFieldService.ComputeFlow(costs, destinationInRegion.X, destinationInRegion.Y);
            }

            var positionInRegion = region.GetCellIndexFromWorldPosition(X, Y);
            var positionInWorld = region.GetCellPositionFromWorldPosition(X, Y);

            _path.Enqueue(positionInWorld);

            while(Destination != positionInWorld)
            {
                var direction = flows[positionInRegion.X, positionInRegion.Y];

                if(direction == Vector2.Zero)
                {
                    break;
                }

                positionInRegion = (positionInRegion.ToVector2() + direction * 1f).ToPoint();
                var newPosition = region.GetCellPositionFromCellIndex(positionInRegion.X, positionInRegion.Y);

                positionInWorld = region.GetCellPositionFromWorldPosition((int)newPosition.X, (int)newPosition.Y);

                _path.Enqueue(positionInWorld);
            }
        }
    }
}
