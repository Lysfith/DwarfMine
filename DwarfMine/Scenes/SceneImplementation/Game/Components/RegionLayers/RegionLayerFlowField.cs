using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfMine.States.StateImplementation.Game.Components.RegionLayers
{
    public class RegionLayerFlowField : RegionLayer
    {
        private Texture2D _texture;

        private int[,] _tileValues;
        private byte[,] _tileCosts;
        private Vector2[,] _flows;
        private Point? _destination;

        public RegionLayerFlowField(Rectangle rectangle)
            : base(LayerType.FLOW_FIELD, rectangle)
        {
            _flows = new Vector2[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
            _tileValues = new int[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
            _tileCosts = new byte[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];

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

        private void ResetValues()
        {
            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    _tileValues[x, y] = 65535;
                }
            }
        }

        public void SetDestination(int x, int y)
        {
            if (_destination == null || _destination.Value.X != x || _destination.Value.Y != y)
            {
                _destination = new Point(x, y);
                Dirty();
            }
        }

        public override void Update(GameTime time)
        {
            if (IsEnabled)
            {
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
            if(!_destination.HasValue)
            {
                return;
            }

            ResetValues();

            //Compute integration field
            var listOpen = new Queue<Point>();

            _tileValues[_destination.Value.X, _destination.Value.Y] = 0;

            listOpen.Enqueue(_destination.Value);

            while(listOpen.Any())
            {
                var currentPosition = listOpen.Dequeue();
                var currentValue = _tileValues[currentPosition.X, currentPosition.Y];
                var currentCost = _tileCosts[currentPosition.X, currentPosition.Y];

                var neighborsN = currentPosition + new Point(0, -1);
                var neighborsS = currentPosition + new Point(0, 1);
                var neighborsW = currentPosition + new Point(-1, 0);
                var neighborsE = currentPosition + new Point(1, 0);

                //unsigned int endNodeCost = getValueByIndex(currentID)                         
                            //+ getCostField()-&gt;getCostByIndex(neighbors[i]);
                if (neighborsN.Y >= 0)
                {
                    var costN = _tileCosts[neighborsN.X, neighborsN.Y];

                    if (costN < 255)
                    {
                        var valueN = currentValue + (currentCost > costN ? currentCost : costN);

                        if (valueN < _tileValues[neighborsN.X, neighborsN.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsN.X && p.Y == neighborsN.Y))
                            {
                                listOpen.Enqueue(neighborsN);
                            }

                            _tileValues[neighborsN.X, neighborsN.Y] = valueN;
                        }
                    }
                }
                if (neighborsS.Y < Constants.REGION_HEIGHT)
                {
                    var costS = _tileCosts[neighborsS.X, neighborsS.Y];

                    if (costS < 255)
                    {
                        var valueS = currentValue + (currentCost > costS ? currentCost : costS);

                        if (valueS < _tileValues[neighborsS.X, neighborsS.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsS.X && p.Y == neighborsS.Y))
                            {
                                listOpen.Enqueue(neighborsS);
                            }

                            _tileValues[neighborsS.X, neighborsS.Y] = valueS;
                        }
                    }
                }
                if (neighborsW.X >= 0)
                {
                    var costW = _tileCosts[neighborsW.X, neighborsW.Y];

                    if (costW < 255)
                    {
                        var valueW = currentValue + (currentCost > costW ? currentCost : costW);

                        if (valueW < _tileValues[neighborsW.X, neighborsW.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsW.X && p.Y == neighborsW.Y))
                            {
                                listOpen.Enqueue(neighborsW);
                            }

                            _tileValues[neighborsW.X, neighborsW.Y] = valueW;
                        }
                    }
                }
                if (neighborsE.X < Constants.REGION_WIDTH)
                {
                    var costE = _tileCosts[neighborsE.X, neighborsE.Y];

                    if (costE < 255)
                    {
                        var valueE = currentValue + (currentCost > costE ? currentCost : costE);

                        if (valueE < _tileValues[neighborsE.X, neighborsE.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsE.X && p.Y == neighborsE.Y))
                            {
                                listOpen.Enqueue(neighborsE);
                            }

                            _tileValues[neighborsE.X, neighborsE.Y] = valueE;
                        }
                    }
                }
            }

            //Compute flow field

            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    _flows[x, y] = ComputeFlowDirection(x, y);
                }
            }

        }

        private Vector2 ComputeFlowDirection(int x, int y)
        {
            var currentPosition = new Point(x, y);
            var neighborsN = currentPosition + new Point(0, -1);
            var neighborsS = currentPosition + new Point(0, 1);
            var neighborsW = currentPosition + new Point(-1, 0);
            var neighborsE = currentPosition + new Point(1, 0);
            var neighborsNE = currentPosition + new Point(1, -1);
            var neighborsNW = currentPosition + new Point(-1, -1);
            var neighborsSE = currentPosition + new Point(1, 1);
            var neighborsSW = currentPosition + new Point(-1, 1);

            var currentValue = _tileValues[x, y];

            if(currentValue == 65535 || currentValue == 0)
            {
                return Vector2.Zero;
            }

            var minValue = int.MaxValue;
            Point? minNeighbors = null;

            if (neighborsN.Y >= 0)
            {
                var value = _tileValues[neighborsN.X, neighborsN.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsN;
                }
            }
            if (neighborsS.Y < Constants.REGION_HEIGHT)
            {
                var value = _tileValues[neighborsS.X, neighborsS.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsS;
                }
            }
            if (neighborsW.X >= 0)
            {
                var value = _tileValues[neighborsW.X, neighborsW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsW;
                }
            }
            if (neighborsE.X < Constants.REGION_WIDTH)
            {
                var value = _tileValues[neighborsE.X, neighborsE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsE;
                }
            }
            if (neighborsNW.X >= 0 && neighborsNW.Y >= 0)
            {
                var value = _tileValues[neighborsNW.X, neighborsNW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsNW;
                }
            }
            if (neighborsNE.X < Constants.REGION_WIDTH && neighborsNE.Y >= 0)
            {
                var value = _tileValues[neighborsNE.X, neighborsNE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsNE;
                }
            }
            if (neighborsSW.X >= 0 && neighborsSW.Y < Constants.REGION_HEIGHT)
            {
                var value = _tileValues[neighborsSW.X, neighborsSW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsSW;
                }
            }
            if (neighborsSE.X < Constants.REGION_WIDTH && neighborsSE.Y < Constants.REGION_HEIGHT)
            {
                var value = _tileValues[neighborsSE.X, neighborsSE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsSE;
                }
            }

            if(minNeighbors != null)
            {
                var v = (minNeighbors.Value.ToVector2() - currentPosition.ToVector2());
                v.Normalize();
                return v;
            }

            return Vector2.Zero;
        }

        private void CreateTexture()
        {
            var spriteBatch = new SpriteBatch(GraphicManager.Instance.GraphicsDevice);
            var customSpriteBatch = new CustomSpriteBatch(spriteBatch);
            var renderTarget = new RenderTarget2D(GraphicManager.Instance.GraphicsDevice, Rectangle.Width, Rectangle.Height);

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            customSpriteBatch.Begin();

            var blanktexture = TextureManager.Instance.GetTexture("blank");

            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    var value = _tileValues[x, y];

                    //var color = new Color(Color.Red, value * 5 / 256f);

                    //customSpriteBatch.Draw(blanktexture, new Rectangle(
                    //    x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT,
                    //    Constants.TILE_WIDTH, Constants.TILE_HEIGHT
                    //    ), color);

                    var direction = _flows[x, y];

                    if (direction != Vector2.Zero)
                    {
                        var currentPosition = new Vector2(x * Constants.TILE_WIDTH + Constants.TILE_HALF_WIDTH, y * Constants.TILE_HEIGHT + Constants.TILE_HALF_HEIGHT);
                        var nextPosition = currentPosition + (direction * new Vector2(Constants.TILE_WIDTH, Constants.TILE_HEIGHT) / 2f);

                        customSpriteBatch.Draw(blanktexture, new Rectangle(
                            (int)currentPosition.X - 3, (int)currentPosition.Y - 3,
                            6, 6
                            ), Color.Cyan);

                        customSpriteBatch.DrawLine(currentPosition, nextPosition, Color.Cyan, 2);
                    }
                }
            }

            customSpriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
