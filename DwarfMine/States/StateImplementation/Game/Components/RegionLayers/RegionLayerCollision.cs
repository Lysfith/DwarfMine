using System;
using System.Collections.Generic;
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

        private bool[,] _collisions;

        public RegionLayerCollision(Rectangle rectangle)
            : base(LayerType.COLLISION, rectangle)
        {
            _collisions = new bool[Constants.REGION_WIDTH, Constants.REGION_HEIGHT];

            _cellSprite = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Yellow, 1);
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

            customSpriteBatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            spriteBatch.Dispose();
        }
    }
}
