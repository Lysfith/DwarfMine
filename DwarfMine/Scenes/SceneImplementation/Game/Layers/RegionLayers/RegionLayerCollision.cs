using System;
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
    public class RegionLayerCollision : RegionLayer
    {
        private Texture2D _texture;
        //private PrimitiveRectangle _cellSprite;
        //private PrimitiveRectangle _cellWalkable;

        private bool[,] _collisions;
        private List<Rectangle> _rectangleWalkables;

        public RegionLayerCollision(Rectangle rectangle)
            : base(LayerType.COLLISION, rectangle)
        {
            _collisions = new bool[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
            _rectangleWalkables = new List<Rectangle>();

            //_cellSprite = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Yellow, 1);
            //_cellWalkable = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Cyan, 1);
        }

        public void SetCollision(int x, int y, bool value)
        {
            var oldValue = _collisions[x, y];

            if (oldValue != value)
            {
                _collisions[x, y] = value;
                Dirty();
            }
        }

        public bool GetCollision(int x, int y)
        {
            return _collisions[x, y];
        }

        public override void Update(GameTime time)
        {
            if (IsEnabled)
            {
                if (IsDirty && IsLoaded)
                {
                    //UpdateWalkableRect();
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

        //private void UpdateWalkableRect()
        //{
        //    _rectangleWalkables.Clear();

        //    var rectangles = Divide(new Rectangle(0, 0, Constants.REGION_WIDTH, Constants.REGION_HEIGHT));

        //    foreach(var rect in rectangles)
        //    {
        //        _rectangleWalkables.Add(new Rectangle(rect.X * Constants.TILE_WIDTH, rect.Y * Constants.TILE_HEIGHT, rect.Width * Constants.TILE_WIDTH, rect.Height * Constants.TILE_HEIGHT));
        //    }

        //    //var cellsClosed = new List<Point>();

        //    //for (int y = 0; y < Constants.REGION_HEIGHT; y++)
        //    //{
        //    //    for (int x = 0; x < Constants.REGION_WIDTH; x++)
        //    //    {
        //    //        var collision = GetCollision(x, y);
        //    //        if (!cellsClosed.Any(c => c.X == x && c.Y == y) && !collision)
        //    //        {
        //    //            var rect = GenerateWalkableRectangle(x, y, 1, cellsClosed);

        //    //            _rectangleWalkables.Add(new Rectangle(rect.X * Constants.TILE_WIDTH, rect.Y * Constants.TILE_HEIGHT, rect.Width * Constants.TILE_WIDTH, rect.Height * Constants.TILE_HEIGHT));

        //    //            for (int rectY = rect.Y; rectY < rect.Y + rect.Height; rectY++)
        //    //            {
        //    //                for (int rectX = rect.X; rectX < rect.X + rect.Width; rectX++)
        //    //                {
        //    //                    //if (!cellsClosed.Any(c => c.X == rectX && c.Y == rectY))
        //    //                    //{
        //    //                        cellsClosed.Add(new Point(rectX, rectY));
        //    //                    //}
        //    //                }
        //    //            }
        //    //        }
        //    //        else if(collision)
        //    //        {
        //    //            cellsClosed.Add(new Point(x, y));
        //    //        }
        //    //    }
        //    //}
        //}

        //private List<Rectangle> Divide(Rectangle parent)
        //{
        //    var result = new List<Rectangle>();

        //    bool hasCollision = false;
        //    for (int y = parent.Y; y < parent.Y + parent.Height; y++)
        //    {
        //        for (int x = parent.X; x < parent.X + parent.Width; x++)
        //        {
        //            if (_collisions[x, y])
        //            {
        //                hasCollision = true;
        //                break;
        //            }
        //        }

        //        if(hasCollision)
        //        {
        //            break;
        //        }
        //    }

        //    if(hasCollision)
        //    {
        //        var width = parent.Width / 2;
        //        var height = parent.Height / 2;
        //        result.AddRange(Divide(new Rectangle(parent.X, parent.Y, width, height)));
        //        result.AddRange(Divide(new Rectangle(parent.X + width, parent.Y, width, height)));
        //        result.AddRange(Divide(new Rectangle(parent.X, parent.Y + width, width, height)));
        //        result.AddRange(Divide(new Rectangle(parent.X + width, parent.Y + height, width, height)));
        //    }
        //    else
        //    {
        //        result.Add(parent);
        //    }

        //    return result;
        //}

        //private Rectangle GenerateWalkableRectangle(int cellX, int cellY, int size, List<Point> cellsClosed)
        //{
        //    var nextX = cellX + size - 1;
        //    var nextY = cellY + size - 1;

        //    if (nextY >= Constants.REGION_HEIGHT || nextX >= Constants.REGION_WIDTH || cellY + size - 1 >= Constants.REGION_HEIGHT || cellX + size - 1 >= Constants.REGION_WIDTH)
        //    {
        //        return new Rectangle(cellX, cellY, size - 1, size - 1);
        //    }

        //    for (int y = cellY; y < cellY + size; y++)
        //    {
        //        if (GetCollision(nextX, y) || cellsClosed.Any(c => c.X == nextX && c.Y == y))
        //        {
        //            return new Rectangle(cellX, cellY, size - 1, size - 1);
        //        }
        //    }

        //    for (int x = cellX; x < cellX + size; x++)
        //    {
        //        if (GetCollision(x, nextY) || cellsClosed.Any(c => c.X == x && c.Y == nextY))
        //        {
        //            return new Rectangle(cellX, cellY, size - 1, size - 1);
        //        }
        //    }

        //    return GenerateWalkableRectangle(cellX, cellY, size + 1, cellsClosed);
        //}

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

            //for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            //{
            //    for (int x = 0; x < Constants.REGION_WIDTH; x++)
            //    {
            //        if (_collisions[x, y])
            //        {
            //            _cellSprite.Draw(customSpriteBatch, x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT);
            //        }
            //    }
            //}

            //foreach (var rect in _rectangleWalkables)
            //{
            //    _cellWalkable.Draw(customSpriteBatch, rect);
            //}

            customSpriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
