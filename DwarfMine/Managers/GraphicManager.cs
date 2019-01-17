using DwarfMine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfMine.Managers
{
    public class GraphicManager
    {
        private static GraphicManager _instance;

        public static GraphicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GraphicManager();
                }

                return _instance;
            }
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public CustomSpriteBatch SpriteBatch { get; private set; }
        public OrthographicCamera Camera { get; private set; }

        public GraphicManager()
        {

        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            GraphicsDevice = device;

            var spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteBatch = new CustomSpriteBatch(spriteBatch);
        }

        public void SetCamera(OrthographicCamera camera)
        {
            Camera = camera;
        }
    }
}
