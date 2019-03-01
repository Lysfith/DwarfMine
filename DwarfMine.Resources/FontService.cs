using DwarfMine.Interfaces;
using DwarfMine.Interfaces.Resource;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DwarfMine.Resources
{
    public class FontService : IFontService
    {
        private Dictionary<string, SpriteFont> _fonts;

        public FontService()
        {
            _fonts = new Dictionary<string, SpriteFont>();
        }

        public SpriteFont GetFont(string name)
        {
            return _fonts[name];
        }

        //public void Load()
        //{
        //    AddFont("Arial-10", content.Load<SpriteFont>("Fonts/Arial-10"));
        //    AddFont("Arial-16", content.Load<SpriteFont>("Fonts/Arial-16"));
        //    AddFont("Arial-24", content.Load<SpriteFont>("Fonts/Arial-24"));
        //    AddFont("Arial-36", content.Load<SpriteFont>("Fonts/Arial-36"));
        //    AddFont("PixelMaster-12", content.Load<SpriteFont>("Fonts/PixelMaster-12"));
        //    AddFont("PixelMaster-16", content.Load<SpriteFont>("Fonts/PixelMaster-16"));
        //    AddFont("PixelMaster-24", content.Load<SpriteFont>("Fonts/PixelMaster-24"));
        //}


        public void AddFont(string name, SpriteFont font)
        {
            if (!_fonts.ContainsKey(name))
            {
                _fonts.Add(name, font);
            }
        }
    }
}
