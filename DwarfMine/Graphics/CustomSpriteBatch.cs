using DwarfMine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfMine.Graphics
{
    public class CustomSpriteBatch
    {
        public int DrawCallsCount { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public CustomSpriteBatch(SpriteBatch spritebatch)
        {
            SpriteBatch = spritebatch;
        }

        public void ResetCounter()
        {
            DrawCallsCount = 0;
        }

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = default(Matrix?))
        {
            SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public void End()
        {
            SpriteBatch.End();
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            SpriteBatch.Draw(texture, destinationRectangle, color);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            SpriteBatch.Draw(texture, position, color);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            SpriteBatch.Draw(texture, position, sourceRectangle, color);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            DrawCallsCount++;
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            DrawCallsCount++;
        }

        public void Draw(Sprite sprite, Transform2 transform)
        {
            SpriteBatch.Draw(sprite, transform);
            DrawCallsCount++;
        }
        public void Draw(Sprite sprite, Vector2 position, float rotation = 0)
        {
            SpriteBatch.Draw(sprite, position, rotation);
            DrawCallsCount++;
        }

        public void Draw(Sprite sprite, Vector2 position, float rotation, Vector2 scale)
        {
            SpriteBatch.Draw(sprite, position, rotation, scale);
            DrawCallsCount++;
        }

        public void DrawHorizontalLine(Vector2 point1, Vector2 point2, Color color, int thickness)
        {
            if (point1.Y != point2.Y)
            {
                return;
            }

            var blankTexture = TextureManager.Instance.GetTexture("blank");

            Draw(blankTexture, new Rectangle((int)point1.X, (int)point1.Y, (int)(point2.X - point1.X), thickness), color);
        }

        public void DrawVerticalLine(Vector2 point1, Vector2 point2, Color color, int thickness)
        {
            if (point1.X != point2.X)
            {
                return;
            }

            var blankTexture = TextureManager.Instance.GetTexture("blank");

            Draw(blankTexture, new Rectangle((int)point1.X, (int)point1.Y, thickness, (int)(point2.Y - point1.Y)), color);
        }

        public void DrawLine(Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0f);
            var scale = new Vector2(length, thickness);
            Draw(TextureManager.Instance.GetTexture("blank"), point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public void DrawRectangle(Rectangle rectangle, int width, Color color)
        {
            var blankTexture = TextureManager.Instance.GetTexture("blank");

            Draw(blankTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, width), color);
            Draw(blankTexture, new Rectangle(rectangle.X, rectangle.Y, width, rectangle.Height), color);
            Draw(blankTexture, new Rectangle(rectangle.X + rectangle.Width - width, rectangle.Y, width, rectangle.Height), color);
            Draw(blankTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - width, rectangle.Width, width), color);
        }

    }
}
