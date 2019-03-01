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
using MonoGame.Extended.Sprites;

namespace DwarfMine.States.StateImplementation.Game.Layers.RegionLayers
{
    public class RegionLayerFloor : RegionLayer
    {
        private Texture2D _texture;

        public RegionLayerFloor(Rectangle rectangle)
            :base(LayerType.FLOOR, rectangle)
        {

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
            Sprite sprite = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var globalService = scope.Resolve<IGlobalService>();
                var spriteService = scope.Resolve<ISpriteService>();

                graphicsDevice = globalService.GetGraphicsDevice();
                sprite = spriteService.GetSprite((int)EnumSprite.GRASS_1);
            }

            var spriteBatch = new SpriteBatch(graphicsDevice);
            var renderTarget = new RenderTarget2D(graphicsDevice, Rectangle.Width, Rectangle.Height);

            graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin();

            for (int y = 0; y < Constants.REGION_HEIGHT; y++)
            {
                for (int x = 0; x < Constants.REGION_WIDTH; x++)
                {
                    spriteBatch.Draw(sprite, new Vector2(x * Constants.TILE_WIDTH + Constants.TILE_WIDTH * 0.5f, y * Constants.TILE_HEIGHT + Constants.TILE_HEIGHT * 0.5f));
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
