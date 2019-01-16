﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfMine.Graphics
{
    public class CustomSpriteBatch : SpriteBatch
    {
        public int DrawCallsCount { get; set; }

        public CustomSpriteBatch(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {

        }

        public new void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = default(Matrix?))
        {
            base.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
            DrawCallsCount = 0;
        }

        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            base.Draw(texture, destinationRectangle, color);
            DrawCallsCount++;
        }


        public new void Draw(Texture2D texture, Vector2 position, Color color)
        {
            base.Draw(texture, position, color);
            DrawCallsCount++;
        }

        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            base.Draw(texture, position, sourceRectangle, color);
            DrawCallsCount++;
        }

        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            base.Draw(texture, destinationRectangle, sourceRectangle, color);
            DrawCallsCount++;
        }

        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            base.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
            DrawCallsCount++;
        }

        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            base.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            DrawCallsCount++;
        }

        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            base.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            DrawCallsCount++;
        }

    }
}
