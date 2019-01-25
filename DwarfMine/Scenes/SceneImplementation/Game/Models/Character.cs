using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.Scenes.SceneImplementation.Game.Systems;
using DwarfMine.States.StateImplementation.Game.Components.RegionLayers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Models
{
    public class Character : Object
    {
        public Point? Destination { get; protected set; }
        public Vector2 Position { get; protected set; }

        public Character(int x, int y)
            :base(x, y, EnumSprite.DEV)
        {
            Position = new Vector2(X, Y);
        }

        public override void Update(GameTime time)
        {
            if(Destination.HasValue && new Point(X, Y) != Destination.Value)
            {
                //Move
                var region = MapSystem.Instance.GetRegion(X, Y);
                var layer = region.GetLayer<RegionLayerFlowField>(RegionLayer.LayerType.FLOW_FIELD);

                var positionInRegion = region.GetCellIndex(X, Y);

                var direction = layer.GetDirection(positionInRegion.X, positionInRegion.Y);

                var nextPosition = Position + direction * (float)time.ElapsedGameTime.TotalSeconds * 100;

                Position = nextPosition;

                X = (int)Position.X;
                Y = (int)Position.Y;
            }
        }

        public override void Draw(CustomSpriteBatch spriteBatch, GameTime time)
        {
            base.Draw(spriteBatch, time);
        }

        public void SetDestination(Point? destination)
        {
            Destination = destination;
        }
    }
}
