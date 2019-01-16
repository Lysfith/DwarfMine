using System;
using System.Collections.Generic;
using System.Text;
using DwarfMine.Graphics;
using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfMine.States.StateImplementation.Game.Components.RegionLayers
{
    public class RegionLayerGrid : RegionLayer
    {
        private Texture2D _texture;
        private PrimitiveRectangle _cellSprite;

        public RegionLayerGrid(Rectangle rectangle)
            :base(LayerType.GRID, rectangle)
        {
            _cellSprite = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.White, 1);
        }

        public override void Update(GameTime time)
        {
            if (IsEnabled)
            {
                if (IsDirty)
                {
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

        private void CreateTexture()
        {
            var spriteBatch = new CustomSpriteBatch(GraphicManager.Instance.GraphicsDevice);
            var renderTarget = new RenderTarget2D(GraphicManager.Instance.GraphicsDevice, Rectangle.Width, Rectangle.Height);

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();

            //var sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_CENTER);

            //for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            //{
            //    for (int x = 0; x < Constants.REGION_WIDTH; x++)
            //    {
            //        _cellSprite.Draw(spriteBatch, x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT);
            //    }
            //}

            var blanktexture = TextureManager.Instance.GetTexture("blank");

            for (int y = 0; y <= Constants.REGION_HEIGHT; y++)
            {
                spriteBatch.DrawLine(new Vector2(0, y * Constants.TILE_HEIGHT), new Vector2(Constants.REGION_WIDTH * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT), Color.White);
            }

            for (int x = 0; x <= Constants.REGION_WIDTH; x++)
            {
                spriteBatch.DrawLine(new Vector2(x * Constants.TILE_WIDTH, 0), new Vector2(x * Constants.TILE_WIDTH, Constants.REGION_HEIGHT * Constants.TILE_HEIGHT), Color.White);
            }

            spriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
