using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game.Components.RegionLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Models
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
                { RegionLayer.LayerType.COLLISION, new RegionLayerCollision(Rectangle) },
                { RegionLayer.LayerType.FLOW_FIELD, new RegionLayerFlowField(Rectangle) },
            };
        }

        public Point GetCellPosition(int xWorld, int yWorld)
        {
            var positionXInRegion = xWorld - X;
            var positionYInRegion = yWorld - Y;

            return new Point(
                (int)(positionXInRegion / Constants.TILE_WIDTH) * Constants.TILE_WIDTH + X + Constants.TILE_HALF_WIDTH, 
                (int)(positionYInRegion / Constants.TILE_HEIGHT) * Constants.TILE_HEIGHT + Y + Constants.TILE_HALF_HEIGHT);
        }

        public Point GetCellIndex(int xWorld, int yWorld)
        {
            var positionXInRegion = xWorld - X;
            var positionYInRegion = yWorld - Y;

            return new Point(
                (int)(positionXInRegion / Constants.TILE_WIDTH),
                (int)(positionYInRegion / Constants.TILE_HEIGHT));
        }

        public bool IsIn(Vector2 point)
        {
            return Rectangle.Contains(point);
        }

        public void Loaded()
        {
            foreach (var layer in _layers)
            {
                layer.Value.Loaded();
            }
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

        public void Select(int x, int y)
        {
            var layer = _layers[RegionLayer.LayerType.FLOW_FIELD] as RegionLayerFlowField;

            layer.SetDestination(x, y);
        }

        public void SetCollision(int x, int y, bool value)
        {
            var layerCollision = _layers[RegionLayer.LayerType.COLLISION] as RegionLayerCollision;
            var layerFlowField = _layers[RegionLayer.LayerType.FLOW_FIELD] as RegionLayerFlowField;

            layerCollision.SetCollision(x, y, value);
            layerFlowField.SetTileCost(x, y, 255);
        }

        public bool GetCollision(int x, int y)
        {
            var layer = _layers[RegionLayer.LayerType.COLLISION] as RegionLayerCollision;

            return layer.GetCollision(x, y);
        }

        public T GetLayer<T>(RegionLayer.LayerType type) where T : RegionLayer
        {
            return _layers[type] as T;
        }
    }
}
