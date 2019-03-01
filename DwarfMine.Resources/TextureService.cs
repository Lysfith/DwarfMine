using DwarfMine.Interfaces.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DwarfMine.Resources
{
    public class TextureService : ITextureService
    {
        private Dictionary<string, Texture2D> _textures;

        public TextureService()
        {
            _textures = new Dictionary<string, Texture2D>();
        }

        public bool Exist(string name)
        {
            return _textures.ContainsKey(name);
        }

        public Texture2D GetTexture(string name)
        {
            Texture2D texture = null;

            if (Exist(name))
            {
                return _textures[name];
            }

            throw new Exception("La texture " + name + " n'existe pas");
        }

        //public override void Load(ContentManager content)
        //{
        //    _content = content;

        //    var blank = new Texture2D(GraphicManager.Instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        //    blank.SetData(new[] { Color.White });

        //    AddTexture("blank", blank);
        //}

        public void AddTexture(string name, Texture2D texture)
        {
            if (!Exist(name))
            {
                _textures.Add(name, texture);
            }
        }
    }
}
