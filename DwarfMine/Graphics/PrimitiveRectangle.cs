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
        private Rectangle _rect;
        private Rectangle _insideRect;
        private Color _fillColor;
        private Color _lineColor;
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
                    spriteBatch.DrawRectangle(_rect, _lineSize, _lineColor);
                    break;
                case Type.FILL_OUTLINE:
                    spriteBatch.Draw(_fillTexture, _insideRect, _fillColor);
                    spriteBatch.DrawRectangle(_rect, _lineSize, _lineColor);
                    break;
            }
        }

        public void Draw(CustomSpriteBatch spriteBatch, Rectangle rectangle)
        {
            switch (_type)
            {
                case Type.FILL:
                    spriteBatch.Draw(_fillTexture, rectangle, _fillColor);
                    break;
                case Type.OUTLINE:
                    spriteBatch.DrawRectangle(rectangle, _lineSize, _lineColor);
                    break;
                case Type.FILL_OUTLINE:
                    var insideRect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                    insideRect.Inflate(-_lineSize, -_lineSize);

                    spriteBatch.Draw(_fillTexture, insideRect, _fillColor);
                    spriteBatch.DrawRectangle(rectangle, _lineSize, _lineColor);
                    break;
            }
        }
    }
}
