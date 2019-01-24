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
        public enum FlowDirection
        {
            TOP_LEFT, TOP, TOP_RIGHT,
            LEFT, CENTER, RIGHT,
            BOTTOM_LEFT, BOTTOM, BOTTOM_RIGHT
        }

        private Texture2D _texture;

        private int[,] _tileCosts;
        private FlowDirection[,] _flows;
        private Point? _destination;

        public RegionLayerFlowField(Rectangle rectangle)
            : base(LayerType.FLOW_FIELD, rectangle)
        {
            _flows = new FlowDirection[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
            _tileCosts = new int[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
        }

        public void SetTileCost(int x, int y, int value)
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

            for(int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    _tileCosts[x, y] = 256;
                }
            }

            var listOpen = new Queue<Point>();

            _tileCosts[_destination.Value.X, _destination.Value.Y] = 0;

            listOpen.Enqueue(_destination.Value);

            while(listOpen.Any())
            {
                var currentPosition = listOpen.Dequeue();
                var currentValue = _tileCosts[currentPosition.X, currentPosition.Y];

                var neighborsN = currentPosition + new Point(0, -1);
                var neighborsS = currentPosition + new Point(0, 1);
                var neighborsW = currentPosition + new Point(-1, 0);
                var neighborsE = currentPosition + new Point(1, 0);

                if(neighborsN.Y > 0)
                {
                    var costN = currentValue + 1 - 1;
                    
                    if (costN < _tileCosts[neighborsN.X, neighborsN.Y])
                    {
                        if(!listOpen.Any(p => p.X == neighborsN.X && p.Y == neighborsN.Y))
                        {
                            listOpen.Enqueue(neighborsN);
                        }

                        _tileCosts[neighborsN.X, neighborsN.Y] = costN;
                    }
                }
                if (neighborsS.Y < Constants.REGION_HEIGHT)
                {
                    var costS = currentValue + 1 - 1;

                    if (costS < _tileCosts[neighborsS.X, neighborsS.Y])
                    {
                        if (!listOpen.Any(p => p.X == neighborsS.X && p.Y == neighborsS.Y))
                        {
                            listOpen.Enqueue(neighborsS);
                        }

                        _tileCosts[neighborsS.X, neighborsS.Y] = costS;
                    }
                }
                if (neighborsW.X > 0)
                {
                    var costW = currentValue + 1 - 1;

                    if (costW < _tileCosts[neighborsW.X, neighborsW.Y])
                    {
                        if (!listOpen.Any(p => p.X == neighborsW.X && p.Y == neighborsW.Y))
                        {
                            listOpen.Enqueue(neighborsW);
                        }

                        _tileCosts[neighborsW.X, neighborsW.Y] = costW;
                    }
                }
                if (neighborsE.X < Constants.REGION_WIDTH)
                {
                    var costE = currentValue + 1 - 1;

                    if (costE < _tileCosts[neighborsE.X, neighborsE.Y])
                    {
                        if (!listOpen.Any(p => p.X == neighborsE.X && p.Y == neighborsE.Y))
                        {
                            listOpen.Enqueue(neighborsE);
                        }

                        _tileCosts[neighborsE.X, neighborsE.Y] = costE;
                    }
                }


            }

            /*
                unsigned int targetID = targetY * mArrayWidth + targetX;
 
                resetField();//Set total cost in all cells to 65535
                list openList;
 
                //Set goal node cost to 0 and add it to the open list
                setValueAt(targetID, 0);
                openList.push_back(targetID);
 
                while (openList.size() &gt; 0)
                {
                    //Get the next node in the open list
                    unsigned currentID = openList.front();
                    openList.pop_front();
 
                    unsigned short currentX = currentID % mArrayWidth;
                    unsigned short currentY = currentID / mArrayWidth;
 
                    //Get the N, E, S, and W neighbors of the current node
                    std::vector neighbors = getNeighbors(currentX, currentY);
                    int neighborCount = neighbors.size();
 
                    //Iterate through the neighbors of the current node
                    for (int i = 0; i &lt; neighborCount; i++)         {             
                        //Calculate the new cost of the neighbor node             
                        // based on the cost of the current node and the weight of the next node             
                        unsigned int endNodeCost = getValueByIndex(currentID)                          
                        + getCostField()-&gt;getCostByIndex(neighbors[i]);
 
                        //If a shorter path has been found, add the node into the open list
                        if (endNodeCost &lt; getValueByIndex(neighbors[i]))
                        {
                            //Check if the neighbor cell is already in the list.
                            //If it is not then add it to the end of the list.
                            if (!checkIfContains(neighbors[i], openList))
                            {
                                openList.push_back(neighbors[i]);
                            }
 
                            //Set the new cost of the neighbor node.
                            setValueAt(neighbors[i], endNodeCost);
                        }
                    }
                }
             */
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
                    var value = _tileCosts[x, y];

                    var color = new Color(Color.Red, value / 256);

                    customSpriteBatch.Draw(blanktexture, new Rectangle(
                        x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT,
                        Constants.TILE_WIDTH, Constants.TILE_HEIGHT
                        ), color);
                }
            }

            //foreach (var rect in _rectangleWalkables)
            //{
            //    _cellWalkable.Draw(customSpriteBatch, rect);
            //}

            customSpriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
