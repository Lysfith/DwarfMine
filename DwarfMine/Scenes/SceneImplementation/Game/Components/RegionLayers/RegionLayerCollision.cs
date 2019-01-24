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
    public class RegionLayerCollision : RegionLayer
    {
        private Texture2D _texture;
        private PrimitiveRectangle _cellSprite;
        private PrimitiveRectangle _cellWalkable;

        private bool[,] _collisions;
        private List<Rectangle> _rectangleWalkables;

        public RegionLayerCollision(Rectangle rectangle)
            : base(LayerType.COLLISION, rectangle)
        {
            _collisions = new bool[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];
            _rectangleWalkables = new List<Rectangle>();

            _cellSprite = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Yellow, 1);
            _cellWalkable = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Cyan, 1);
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
                    UpdateWalkableRect();
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

        private void UpdateWalkableRect()
        {
            _rectangleWalkables.Clear();

            var cellsClosed = new List<Point>();

            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    var collision = GetCollision(x, y);
                    if (!cellsClosed.Any(c => c.X == x && c.Y == y) && !collision)
                    {
                        var rect = GenerateWalkableRectangle(x, y, 1, cellsClosed);

                        _rectangleWalkables.Add(new Rectangle(rect.X * Constants.TILE_WIDTH, rect.Y * Constants.TILE_HEIGHT, rect.Width * Constants.TILE_WIDTH, rect.Height * Constants.TILE_HEIGHT));

                        for (int rectY = rect.Y; rectY < rect.Y + rect.Height; rectY++)
                        {
                            for (int rectX = rect.X; rectX < rect.X + rect.Width; rectX++)
                            {
                                //if (!cellsClosed.Any(c => c.X == rectX && c.Y == rectY))
                                //{
                                    cellsClosed.Add(new Point(rectX, rectY));
                                //}
                            }
                        }
                    }
                    else if(collision)
                    {
                        cellsClosed.Add(new Point(x, y));
                    }
                }
            }
        }

        private Rectangle GenerateWalkableRectangle(int cellX, int cellY, int size, List<Point> cellsClosed)
        {
            var nextX = cellX + size - 1;
            var nextY = cellY + size - 1;

            if (nextY >= Constants.REGION_HEIGHT || nextX >= Constants.REGION_WIDTH || cellY + size - 1 >= Constants.REGION_HEIGHT || cellX + size - 1 >= Constants.REGION_WIDTH)
            {
                return new Rectangle(cellX, cellY, size - 1, size - 1);
            }

            for (int y = cellY; y < cellY + size; y++)
            {
                if (GetCollision(nextX, y) || cellsClosed.Any(c => c.X == nextX && c.Y == y))
                {
                    return new Rectangle(cellX, cellY, size - 1, size - 1);
                }
            }

            for (int x = cellX; x < cellX + size; x++)
            {
                if (GetCollision(x, nextY) || cellsClosed.Any(c => c.X == x && c.Y == nextY))
                {
                    return new Rectangle(cellX, cellY, size - 1, size - 1);
                }
            }

            return GenerateWalkableRectangle(cellX, cellY, size + 1, cellsClosed);
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
                    if (_collisions[x, y])
                    {
                        _cellSprite.Draw(customSpriteBatch, x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT);
                    }
                }
            }

            foreach (var rect in _rectangleWalkables)
            {
                _cellWalkable.Draw(customSpriteBatch, rect);
            }

            customSpriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
