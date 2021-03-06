﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Resource;
using DwarfMine.Interfaces.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfMine.States.StateImplementation.Game.Layers.RegionLayers
{
    public class RegionLayerFlowField : RegionLayer
    {
        public enum Direction {
            NORTH,
            SOUTH,
            WEST,
            EAST,
            NORTH_WEST,
            NORTH_EAST,
            SOUTH_WEST,
            SOUTH_EAST,
            CENTER
        }

        private Texture2D _texture;

        private byte[,] _tileCosts;
        private Dictionary<Direction, Vector2[,]> _flowDirections;
        private Point? _destination;
        private bool _destinationHasChanged = false;

        public RegionLayerFlowField(Rectangle rectangle)
            : base(LayerType.FLOW_FIELD, rectangle)
        {
            _tileCosts = new byte[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];

            _flowDirections = new Dictionary<Direction, Vector2[,]>();

            ResetCosts();
        }

        public void SetTileCost(int x, int y, byte value)
        {
            var oldValue = _tileCosts[x, y];

            if (oldValue != value)
            {
                _tileCosts[x, y] = value;
                Dirty();
            }
        }

        public int GetTileCost(int x, int y)
        {
            return _tileCosts[x, y];
        }

        public byte[,] GetCosts()
        {
            return _tileCosts;
        }

        private void ResetCosts()
        {
            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    _tileCosts[x, y] = 1;
                }
            }
        }

        public Vector2 GetDirection(int x, int y)
        {
            if (_flowDirections.ContainsKey(Direction.CENTER))
            {
                var flow = _flowDirections[Direction.CENTER];
                return flow[x, y];
            }

            return Vector2.Zero;
        }

        public void SetDestination(int x, int y)
        {
            if (_destination == null || _destination.Value.X != x || _destination.Value.Y != y)
            {
                _destination = new Point(x, y);
                _destinationHasChanged = true;
            }
        }

        public override void Update(GameTime time)
        {
            if (IsEnabled)
            {
                if (_destinationHasChanged && IsLoaded)
                {
                    UpdateFlowInRegion();

                    if(!IsDirty)
                    {
                        CreateTexture();
                    }

                    _destinationHasChanged = false;
                }

                if (IsDirty && IsLoaded)
                {
                    UpdateFlow();
                    CreateTexture();

                    IsDirty = false;
                }
            }
        }

        public override void Draw(GameTime time, CustomSpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                if (_texture != null)
                {
                    spriteBatch.Draw(_texture, Rectangle, Color.White);
                }
            }
        }

        public override void DisposeResources()
        {
            if (_texture != null)
            {
                _texture.Dispose();
                _texture = null;
            }
        }

        private void UpdateFlow()
        {
            _flowDirections.Clear();

            //_flowDirections.Add(Direction.NORTH_WEST, PathFindingService.ComputeFlow(_tileCosts, 0, 0));
            //_flowDirections.Add(Direction.NORTH, PathFindingService.ComputeFlow(_tileCosts, Constants.REGION_WIDTH / 2, 0));
            //_flowDirections.Add(Direction.NORTH_EAST, PathFindingService.ComputeFlow(_tileCosts, Constants.REGION_WIDTH-1, 0));
            //_flowDirections.Add(Direction.EAST, PathFindingService.ComputeFlow(_tileCosts, 0, Constants.REGION_HEIGHT / 2));
            //_flowDirections.Add(Direction.WEST, PathFindingService.ComputeFlow(_tileCosts, Constants.REGION_WIDTH-1, Constants.REGION_HEIGHT / 2));
            //_flowDirections.Add(Direction.SOUTH_WEST, PathFindingService.ComputeFlow(_tileCosts, 0, Constants.REGION_HEIGHT-1));
            //_flowDirections.Add(Direction.SOUTH, PathFindingService.ComputeFlow(_tileCosts, Constants.REGION_WIDTH / 2, Constants.REGION_HEIGHT-1));
            //_flowDirections.Add(Direction.SOUTH_EAST, PathFindingService.ComputeFlow(_tileCosts, Constants.REGION_WIDTH-1, Constants.REGION_HEIGHT-1));
        }

        private void UpdateFlowInRegion()
        {
            if (!_destination.HasValue)
            {
                return;
            }

            //if(_flowDirections.ContainsKey(Direction.CENTER))
            //{
            //    _flowDirections[Direction.CENTER] = PathFindingService.ComputeFlow(_tileCosts, _destination.Value.X, _destination.Value.Y);
            //}
            //else
            //{
            //    _flowDirections.Add(Direction.CENTER, PathFindingService.ComputeFlow(_tileCosts, _destination.Value.X, _destination.Value.Y));
            //}
        }

        private void CreateTexture()
        {
            GraphicsDevice graphicsDevice = null;
            Texture2D blanktexture = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var globalService = scope.Resolve<IGlobalService>();
                var textureService = scope.Resolve<ITextureService>();

                graphicsDevice = globalService.GetGraphicsDevice();
                blanktexture = textureService.GetTexture("blank");
            }


            var spriteBatch = new SpriteBatch(graphicsDevice);
            var customSpriteBatch = new CustomSpriteBatch(spriteBatch);
            var renderTarget = new RenderTarget2D(graphicsDevice, Rectangle.Width, Rectangle.Height);

            graphicsDevice.SetRenderTarget(renderTarget);

            graphicsDevice.Clear(Color.Transparent);

            customSpriteBatch.Begin();

            //if (_tileCosts != null)
            //{
            //    var color = Color.Red;

            //    for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            //    {
            //        for (int x = 0; x < Constants.REGION_WIDTH; x++)
            //        {
            //            var tile = _tileCosts[x, y];

            //            var currentPosition = new Vector2(x * Constants.TILE_WIDTH + Constants.TILE_HALF_WIDTH, y * Constants.TILE_HEIGHT + Constants.TILE_HALF_HEIGHT);

            //            color.A = tile;

            //            customSpriteBatch.Draw(blanktexture, new Rectangle(
            //                (int)currentPosition.X - Constants.TILE_HALF_WIDTH, (int)currentPosition.Y - Constants.TILE_HALF_HEIGHT,
            //                Constants.TILE_WIDTH, Constants.TILE_HEIGHT
            //                ), color);
            //        }
            //    }
            //}

            //if (_flowDirections.ContainsKey(Direction.CENTER))
            //{
            //    var flow = _flowDirections[Direction.CENTER];

            //    for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            //    {
            //        for (int x = 0; x < Constants.REGION_WIDTH; x++)
            //        {
            //            var direction = flow[x, y];

            //            if (direction != Vector2.Zero)
            //            {
            //                var currentPosition = new Vector2(x * Constants.TILE_WIDTH + Constants.TILE_HALF_WIDTH, y * Constants.TILE_HEIGHT + Constants.TILE_HALF_HEIGHT);
            //                var nextPosition = currentPosition + (direction * new Vector2(Constants.TILE_WIDTH, Constants.TILE_HEIGHT) / 2f);

            //                customSpriteBatch.Draw(blanktexture, new Rectangle(
            //                    (int)currentPosition.X - 3, (int)currentPosition.Y - 3,
            //                    6, 6
            //                    ), Color.Orange);

            //                customSpriteBatch.DrawLine(blanktexture, currentPosition, nextPosition, Color.Orange, 2);
            //            }
            //        }
            //    }
            //}

            customSpriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
