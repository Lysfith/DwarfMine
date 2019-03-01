using DwarfMine.Interfaces.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Resources
{
    public class SpriteService : ISpriteService
    {
        private static SpriteService _instance;

        public static SpriteService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpriteService();
                }

                return _instance;
            }
        }

        private Dictionary<int, Sprite> _sprites;

        public SpriteService()
        {
            _sprites = new Dictionary<int, Sprite>();
        }

        public bool Exist(int index)
        {
            return _sprites.ContainsKey(index);
        }

        public Sprite GetSprite(int index)
        {
            if (Exist(index))
            {
                return _sprites[index];
            }

            throw new Exception("Le sprite " + index + " n'existe pas");
        }

        public void AddOrReplaceSprite(int index, Sprite sprite)
        {
            if (Exist(index))
            {
                _sprites[index] = sprite;
            }
            else
            {
                _sprites.Add(index, sprite);
            }
        }

        //public override void Load(ContentManager content)
        //{
        //    var directory = "Textures/Game/";

        //    var tiles = TextureService.Instance.GetTexture($"{directory}tiles");

        //    AddOrReplaceSprite(EnumSprite.DEV, new Sprite(new TextureRegion2D(tiles, new Rectangle(0, 0, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.DEV2, new Sprite(new TextureRegion2D(tiles, new Rectangle(32, 0, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.SELECT, new Sprite(new TextureRegion2D(tiles, new Rectangle(0, 32, 32, 32))));


        //    //var outdoors = TextureManager.Instance.GetTexture($"{directory}OutdoorsTileset");
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 0, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 0, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_TOP_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 0, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 16, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_CENTER, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 16, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 16, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM_LEFT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(0, 16 * 2, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16, 16 * 2, 16, 16))));
        //    //AddOrReplaceSprite(EnumSprite.GRASS_INNER_BOTTOM_RIGHT, new Sprite(new TextureRegion2D(outdoors, new Rectangle(16 * 2, 16 * 2, 16, 16))));

        //    var tiles2 = TextureService.Instance.GetTexture($"{directory}tiles-2");
            
        //    AddOrReplaceSprite(EnumSprite.GRASS_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 0, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.TREE_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 192, 64, 64))) { Origin = new Vector2(32, 48) });
        //    AddOrReplaceSprite(EnumSprite.FLOWER_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 96, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.FLOWER_2, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32, 96, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.FLOWER_3, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 2, 96, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.FLOWER_4, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 3, 96, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.FLOWER_5, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 4, 96, 32, 32))));
        //    AddOrReplaceSprite(EnumSprite.FLOWER_6, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 5, 96, 32, 32))));
        //}
    }
}
