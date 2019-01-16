using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Graphics
{
    public class PrimitiveRectangle
    {
        public enum Type { FILL, OUTLINE, FILL_OUTLINE };
        public Rectangle _rect;
        public Rectangle _insideRect;
        public Color _fillColor;
        public Color _lineColor;
        private Type _type;
        private int _lineSize;
        private Texture2D _fillTexture;

        public PrimitiveRectangle(Type type, int width, int height, Texture2D fillTexture, Color fillColor, Color lineColor, int lineSize)
        {
            _rect = new Rectangle(0, 0, width, height);
            _insideRect = new Rectangle(0, 0, width, height);
            _insideRect.Inflate(-lineSize, -lineSize);
            _type = type;
            _fillColor = fillColor;
            _lineColor = lineColor;
            _lineSize = lineSize;
        }

        public void Draw(CustomSpriteBatch spriteBatch, int x, int y)
        {
            _rect.X = x;
            _rect.Y = y;

            _insideRect.X = x + _lineSize;
            _insideRect.Y = y + _lineSize;

            switch (_type)
            {
                case Type.FILL:
                    spriteBatch.Draw(_fillTexture, _rect, _fillColor);
                    break;
                case Type.OUTLINE:
                    DrawOutline(spriteBatch, x, y);
                    break;
                case Type.FILL_OUTLINE:
                    spriteBatch.Draw(_fillTexture, _insideRect, _fillColor);
                    DrawOutline(spriteBatch, x, y);
                    break;
            }
        }

        private void DrawOutline(CustomSpriteBatch spriteBatch, int x, int y)
        {
            var position = new Vector2(_rect.X, _rect.Y);

            spriteBatch.DrawLine(position, position + new Vector2(_rect.Width, 0), _lineColor, _lineSize);
            spriteBatch.DrawLine(position, position + new Vector2(0, _rect.Height), _lineColor, _lineSize);
            spriteBatch.DrawLine(position + new Vector2(_rect.Width, 0), position + new Vector2(_rect.Width, _rect.Height), _lineColor, _lineSize);
            spriteBatch.DrawLine(position + new Vector2(0, _rect.Height), position + new Vector2(_rect.Width, _rect.Height), _lineColor, _lineSize);
        }
    }
}
