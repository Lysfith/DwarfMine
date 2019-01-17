using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game.Components.RegionLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
{
    public class Region
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get { return Constants.REGION_WIDTH * Constants.TILE_WIDTH; } }
        public int Height { get { return Constants.REGION_HEIGHT * Constants.TILE_HEIGHT; } }
        public Rectangle Rectangle { get; private set; }
        public bool PreviousVisibility { get; private set; }
        public bool Visibility { get; private set; }


        private Dictionary<RegionLayer.LayerType, RegionLayer> _layers;

        public Region(int x, int y)
        {
            X = x * Width;
            Y = y * Height;
            Rectangle = new Rectangle(X, Y, Width, Height);

            _layers = new Dictionary<RegionLayer.LayerType, RegionLayer>()
            {
                { RegionLayer.LayerType.FLOOR, new RegionLayerFloor(Rectangle) },
                //{ RegionLayer.LayerType.GRID, new RegionLayerGrid(Rectangle) },
            };
        }

        public Point GetCellPosition(int xWorld, int yWorld)
        {
            var positionXInRegion = xWorld - X;
            var positionYInRegion = yWorld - Y;

            return new Point(
                (int)(positionXInRegion / Constants.TILE_WIDTH) * Constants.TILE_WIDTH + X, 
                (int)(positionYInRegion / Constants.TILE_HEIGHT) * Constants.TILE_HEIGHT + Y);
        }

        public bool IsIn(Vector2 point)
        {
            return Rectangle.Contains(point);
        }

        public void Update(GameTime time)
        {
            if (PreviousVisibility != Visibility)
            {
                if (Visibility)
                {
                    foreach (var layer in _layers)
                    {
                        layer.Value.Dirty();
                        layer.Value.Update(time);
                    }
                }
                else
                {
                    foreach (var layer in _layers)
                    {
                        layer.Value.DisposeResources();
                        layer.Value.Update(time);
                    }
                }
            }
            else
            {
                foreach (var layer in _layers)
                {
                    layer.Value.Update(time);
                }
            }
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch)
        {
            foreach(var layer in _layers)
            {
                layer.Value.Draw(time, spriteBatch);
            }
        }

        public void SetVisible(bool value)
        {
            PreviousVisibility = Visibility;
            Visibility = value;
        }
    }
}
