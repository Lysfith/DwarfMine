using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DwarfMine.Managers
{
    public class TextureManager : BaseManager
    {
        private static TextureManager _instance;

        public static TextureManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TextureManager();
                }

                return _instance;
            }
        }

        private ContentManager _content;

        private Dictionary<string, Texture2D> _textures;

        public TextureManager()
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
                texture = _textures[name];
            }
            else
            {
                texture = _content.Load<Texture2D>(name);
                _textures.Add(name, texture);
            }

            return texture;
        }

        public override void Load(ContentManager content)
        {
            _content = content;

            var blank = new Texture2D(GraphicManager.Instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            AddTexture("blank", blank);
        }

        private void AddTexture(string name, Texture2D texture)
        {
            if (!Exist(name))
            {
                _textures.Add(name, texture);
            }
        }
    }
}
