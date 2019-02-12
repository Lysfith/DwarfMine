using DwarfMine.States.StateImplementation.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Managers
{
    public class SpriteManager : BaseManager
    {
        private static SpriteManager _instance;

        public static SpriteManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpriteManager();
                }

                return _instance;
            }
        }

        private Dictionary<EnumSprite, Sprite> _sprites;

        public SpriteManager()
        {
            _sprites = new Dictionary<EnumSprite, Sprite>();
        }

        public bool Exist(EnumSprite name)
        {
            return _sprites.ContainsKey(name);
        }

        public Sprite GetSprite(EnumSprite name)
        {
            if (Exist(name))
            {
                return _sprites[name];
            }

            throw new Exception("Le sprite " + name + " n'existe pas");
        }

        public void AddOrReplaceSprite(EnumSprite name, Sprite sprite)
        {
            if (Exist(name))
            {
                _sprites[name] = sprite;
            }
            else
            {
                _sprites.Add(name, sprite);
            }
        }

        public override void Load(ContentManager content)
        {
            var directory = "Textures/Game/";

            var tiles = TextureManager.Instance.GetTexture($"{directory}tiles");

            AddOrReplaceSprite(EnumSprite.DEV, new Sprite(new TextureRegion2D(tiles, new Rectangle(0, 0, 16, 16))));
            AddOrReplaceSprite(EnumSprite.DEV2, new Sprite(new TextureRegion2D(tiles, new Rectangle(16, 0, 16, 16))));
           

            //var outdoors = TextureManager.Instance.GetTexture($"{directory}OutdoorsTileset");
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 0, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 0, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 0, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 16, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_CENTER, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 16, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 16, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 16 * 2, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 16 * 2, 16, 16))));
            //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 16 * 2, 16, 16))));

            var tiles2 = TextureManager.Instance.GetTexture($"{directory}tiles-2");
            
            AddOrReplaceSprite(EnumSprite.GRASS_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 0, 32, 32))));
            AddOrReplaceSprite(EnumSprite.TREE_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 192, 64, 64))) { Origin = new Vector2(32, 48) });
            AddOrReplaceSprite(EnumSprite.FLOWER_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 96, 32, 32))));
            AddOrReplaceSprite(EnumSprite.FLOWER_2, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32, 96, 32, 32))));
            AddOrReplaceSprite(EnumSprite.FLOWER_3, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 2, 96, 32, 32))));
            AddOrReplaceSprite(EnumSprite.FLOWER_4, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 3, 96, 32, 32))));
            AddOrReplaceSprite(EnumSprite.FLOWER_5, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 4, 96, 32, 32))));
            AddOrReplaceSprite(EnumSprite.FLOWER_6, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 5, 96, 32, 32))));
        }
    }
}
