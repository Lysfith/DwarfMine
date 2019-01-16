using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
{
    public class RegionTile
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get { return Constants.REGION_TILE_WIDTH * Constants.TILE_WIDTH; } }
        public int Height { get { return Constants.REGION_TILE_HEIGHT * Constants.TILE_HEIGHT; } }
        public Rectangle Rectangle { get; private set; }
        public Texture2D Texture { get; private set; }

        private EnumTileType[,] Tiles;

        public RegionTile(int x, int y)
        {
            X = x * Width;
            Y = y * Height;
            Rectangle = new Rectangle(X, Y, Width, Height);
            Tiles = new EnumTileType[Constants.REGION_TILE_WIDTH, Constants.REGION_TILE_HEIGHT];
        }

        public void SetTile(int x, int y, EnumTileType type)
        {
            Tiles[x, y] = type;
        }

        public bool IsIn(Vector2 point)
        {
            return Rectangle.Contains(point);
        }

        public void Apply()
        {
            CreateTexture();
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch)
        {
            if(Texture != null)
            {
                spriteBatch.Draw(Texture, Rectangle, Color.White);
            }
        }

        private void CreateTexture()
        {
            var spriteBatch = new CustomSpriteBatch(GraphicManager.Instance.GraphicsDevice);
            var renderTarget = new RenderTarget2D(GraphicManager.Instance.GraphicsDevice, Width, Height);

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);

            var positionMin = new Vector2(X, Y);
            var positionMax = new Vector2(X + Width, Y + Height);

            spriteBatch.Begin();

            for(int y = 0; y < Constants.REGION_TILE_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_TILE_WIDTH; x++)
                {
                    Sprite sprite = null;

                    if(x % 3 == 0)
                    {
                        if (y % 3 == 0)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_TOP_LEFT);
                        }
                        else if (y % 3 == 1)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_LEFT);
                        }
                        else if(y % 3 == 2)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_BOTTOM_LEFT);
                        }
                    }
                    else if (x % 3 == 1)
                    {
                        if (y % 3 == 0)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_TOP);
                        }
                        else if (y % 3 == 1)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_CENTER);
                        }
                        else if (y % 3 == 2)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_BOTTOM);
                        }
                    }
                    else if (x % 3 == 2)
                    {
                        if (y % 3 == 0)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_TOP_RIGHT);
                        }
                        else if (y % 3 == 1)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_RIGHT);
                        }
                        else if (y % 3 == 2)
                        {
                            sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_BOTTOM_RIGHT);
                        }
                    }

                    if (sprite != null)
                    {
                        spriteBatch.Draw(sprite, new Vector2(x * Constants.TILE_WIDTH + Constants.TILE_WIDTH * 0.5f, y * Constants.TILE_HEIGHT + Constants.TILE_HEIGHT * 0.5f));
                    }
                }
            }

            spriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            Texture = (Texture2D)renderTarget;
        }
    }
}
