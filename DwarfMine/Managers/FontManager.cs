using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfMine.Managers
{
    public class FontManager : BaseManager
    {
        private static FontManager _instance;

        public static FontManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FontManager();
                }

                return _instance;
            }
        }

        private Dictionary<string, SpriteFont> _fonts;

        public FontManager()
        {
            _fonts = new Dictionary<string, SpriteFont>();
        }

        public SpriteFont GetFont(string name)
        {
            return _fonts[name];
        }

        public override void Load(ContentManager content)
        {
            AddFont("Arial-10", content.Load<SpriteFont>("Fonts/Arial-10"));
            AddFont("Arial-16", content.Load<SpriteFont>("Fonts/Arial-16"));
            AddFont("Arial-24", content.Load<SpriteFont>("Fonts/Arial-24"));
            AddFont("Arial-36", content.Load<SpriteFont>("Fonts/Arial-36"));
            AddFont("PixelMaster-12", content.Load<SpriteFont>("Fonts/PixelMaster-12"));
            AddFont("PixelMaster-16", content.Load<SpriteFont>("Fonts/PixelMaster-16"));
            AddFont("PixelMaster-24", content.Load<SpriteFont>("Fonts/PixelMaster-24"));
        }


        private void AddFont(string name, SpriteFont font)
        {
            if (!_fonts.ContainsKey(name))
            {
                _fonts.Add(name, font);
            }
        }
    }
}
