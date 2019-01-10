using DwarfMine.Graphics;
using Microsoft.Xna.Framework.Graphics;
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

        public GraphicManager()
        {

        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            GraphicsDevice = device;

            SpriteBatch = new CustomSpriteBatch(GraphicsDevice);
        }
    }
}
