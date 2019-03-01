using System;
using System.Collections.Generic;
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
    public class RegionLayerGrid : RegionLayer
    {
        private Texture2D _texture;
        //private PrimitiveRectangle _cellSprite;

        public RegionLayerGrid(Rectangle rectangle)
            :base(LayerType.GRID, rectangle)
        {
            //_cellSprite = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.White, 1);
        }

        public override void Update(GameTime time)
        {
            if (IsEnabled)
            {
                if (IsDirty && IsLoaded)
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

            //var sprite = SpriteManager.Instance.GetSprite(EnumSprite.GRASS_INNER_CENTER);

            //for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            //{
            //    for (int x = 0; x < Constants.REGION_WIDTH; x++)
            //    {
            //        _cellSprite.Draw(spriteBatch, x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT);
            //    }
            //}

            for (int y = 0; y <= Constants.REGION_HEIGHT; y++)
            {

                customSpriteBatch.DrawHorizontalLine(blanktexture, new Vector2(0, y * Constants.TILE_HEIGHT), new Vector2(Constants.REGION_WIDTH * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT), Color.White, 1);

                if (y > 0)
                {
                    customSpriteBatch.DrawHorizontalLine(blanktexture, new Vector2(0, y * Constants.TILE_HEIGHT-1), new Vector2(Constants.REGION_WIDTH * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT-1), Color.White, 1);
                }
            }

            for (int x = 0; x <= Constants.REGION_WIDTH; x++)
            {
                customSpriteBatch.DrawVerticalLine(blanktexture, new Vector2(x * Constants.TILE_WIDTH, 0), new Vector2(x * Constants.TILE_WIDTH, Constants.REGION_HEIGHT * Constants.TILE_HEIGHT), Color.White, 1);

                if (x > 0)
                {
                    customSpriteBatch.DrawVerticalLine(blanktexture, new Vector2(x * Constants.TILE_WIDTH-1, 0), new Vector2(x * Constants.TILE_WIDTH-1, Constants.REGION_HEIGHT * Constants.TILE_HEIGHT), Color.White, 1);
                }
            }

            customSpriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
