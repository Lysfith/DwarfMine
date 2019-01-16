using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DwarfMine.Managers
{
    public class AssetManager : BaseManager
    {
        private static AssetManager _instance;

        public static AssetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetManager();
                }

                return _instance;
            }
        }


        public SpriteFont MainFont { get; private set; }

        public AssetManager()
        {
            
        }

        public override void Load(ContentManager content)
        {
            MainFont = FontManager.Instance.GetFont("PixelMaster-24");
        }
    }
}
