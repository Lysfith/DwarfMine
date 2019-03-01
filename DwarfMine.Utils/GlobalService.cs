using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Util;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Utils
{
    public class GlobalService : IGlobalService
    {
        private OrthographicCamera _camera;
        private ContentManager _content;
        private GraphicsDevice _graphicsDevice;
        private CustomSpriteBatch _spriteBatch;

        public GlobalService()
        {

        }

        public void Set(OrthographicCamera camera, ContentManager content, GraphicsDevice graphicsDevice, CustomSpriteBatch spriteBatch)
        {
            _camera = camera;
            _content = content;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
        }

        public OrthographicCamera GetCamera()
        {
            return _camera;
        }

        public ContentManager GetContentManager()
        {
            return _content;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return _graphicsDevice;
        }

        public CustomSpriteBatch GetSpriteBatch()
        {
            return _spriteBatch;
        }
    }
}
