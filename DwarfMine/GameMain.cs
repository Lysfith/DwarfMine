using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Debug;
using DwarfMine.Interfaces.Debug;
using DwarfMine.Interfaces.Resource;
using DwarfMine.Interfaces.Util;
using DwarfMine.Resources;
using DwarfMine.States;
using DwarfMine.States.StateImplementation.Game;
using DwarfMine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine
{
    public class GameMain : GameBase
    {
        public GameMain()
        {
        }

        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            var globalService = new GlobalService();

            globalService.Set(new OrthographicCamera(GraphicsDevice), Content, GraphicsDevice, new CustomSpriteBatch(new SpriteBatch(GraphicsDevice)));

            builder.RegisterInstance<IGlobalService>(globalService);

            //Resources

            var fontService = new FontService();

            fontService.AddFont("Arial-10", Content.Load<SpriteFont>("Fonts/Arial-10"));
            fontService.AddFont("Arial-16", Content.Load<SpriteFont>("Fonts/Arial-16"));
            fontService.AddFont("Arial-24", Content.Load<SpriteFont>("Fonts/Arial-24"));
            fontService.AddFont("Arial-36", Content.Load<SpriteFont>("Fonts/Arial-36"));
            fontService.AddFont("PixelMaster-12", Content.Load<SpriteFont>("Fonts/PixelMaster-12"));
            fontService.AddFont("PixelMaster-16", Content.Load<SpriteFont>("Fonts/PixelMaster-16"));
            fontService.AddFont("PixelMaster-24", Content.Load<SpriteFont>("Fonts/PixelMaster-24"));

            builder.RegisterInstance<IFontService>(fontService);

            var textureService = new TextureService();

            var blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            textureService.AddTexture("blank", blank);

            textureService.AddTexture($"tiles", Content.Load<Texture2D>("Textures/Game/tiles"));
            textureService.AddTexture($"tiles-2", Content.Load<Texture2D>("Textures/Game/tiles-2"));
            textureService.AddTexture($"utils", Content.Load<Texture2D>("Textures/Game/utils"));
            textureService.AddTexture($"objects", Content.Load<Texture2D>("Textures/Game/objects"));
            textureService.AddTexture($"characters", Content.Load<Texture2D>("Textures/Game/characters"));

            builder.RegisterInstance<ITextureService>(textureService);

            var spriteService = new SpriteService();

            var tiles = textureService.GetTexture($"tiles");

            spriteService.AddOrReplaceSprite((int)EnumSprite.DEV, new Sprite(new TextureRegion2D(tiles, new Rectangle(0, 0, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.DEV2, new Sprite(new TextureRegion2D(tiles, new Rectangle(32, 0, 32, 32))));

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

            var tiles2 = textureService.GetTexture($"tiles-2");

            spriteService.AddOrReplaceSprite((int)EnumSprite.GRASS_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 0, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.TREE_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 192, 64, 64))) { Origin = new Vector2(32, 48) });
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_1, new Sprite(new TextureRegion2D(tiles2, new Rectangle(0, 96, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_2, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32, 96, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_3, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 2, 96, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_4, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 3, 96, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_5, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 4, 96, 32, 32))));
            spriteService.AddOrReplaceSprite((int)EnumSprite.FLOWER_6, new Sprite(new TextureRegion2D(tiles2, new Rectangle(32 * 5, 96, 32, 32))));

            var objects = textureService.GetTexture($"objects");

            spriteService.AddOrReplaceSprite((int)EnumSprite.ROCK_1, new Sprite(new TextureRegion2D(objects, new Rectangle(0, 0, 32, 32))));

            var characters = textureService.GetTexture($"characters");

            spriteService.AddOrReplaceSprite((int)EnumSprite.CHARACTER_1, new Sprite(new TextureRegion2D(characters, new Rectangle(0, 0, 32, 32))));

            var utils = textureService.GetTexture($"utils");

            var animation = new SpriteSheetAnimationFactory(new List<TextureRegion2D>() {
                new TextureRegion2D(utils, new Rectangle(0, 0, 32, 32)),
                new TextureRegion2D(utils, new Rectangle(32, 0, 32, 32)),
                new TextureRegion2D(utils, new Rectangle(32 * 2, 0, 32, 32)),
                new TextureRegion2D(utils, new Rectangle(32 * 3, 0, 32, 32))
            });

            animation.Add("default", new SpriteSheetAnimationData(new int[] { 0, 1, 2, 3, 2, 1 }, 0.5f));

            var animatedSprite = new AnimatedSprite(animation);

            spriteService.AddOrReplaceAnimatedSprite((int)EnumSpriteAnimated.CURSOR, animatedSprite);

            builder.RegisterInstance<ISpriteService>(spriteService);



            builder.RegisterInstance<IJobService>(new JobService());

            builder.RegisterType<FlowFieldService>().As<IFlowFieldService>();
            builder.RegisterType<LogService>().As<ILogService>();
            builder.RegisterType<DebugService>().As<IDebugService>();
        }

        protected override void LoadContent()
        {
            SceneManager.Instance.SetScene(EnumScene.Game);
        }

        protected override void Update(GameTime gameTime)
        {
            using (var scope = GameCore.Instance.CreateScope())
            {
                var debugService = scope.Resolve<IDebugService>();
                var jobService = scope.Resolve<IJobService>();
                var globalService = scope.Resolve<IGlobalService>();

                debugService.StartUpdate();

                jobService.Update(gameTime);

                SceneManager.Instance.Update(gameTime, globalService.GetCamera());

                debugService.StopUpdate();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            using (var scope = GameCore.Instance.CreateScope())
            {
                var debugService = scope.Resolve<IDebugService>();
                var globalService = scope.Resolve<IGlobalService>();

                var spriteBatch = globalService.GetSpriteBatch();

                spriteBatch.ResetCounter();

                GraphicsDevice.Clear(Color.CornflowerBlue);

                debugService.StartDraw();

                SceneManager.Instance.Draw(gameTime, spriteBatch, globalService.GetCamera());

                debugService.StopDraw();

                debugService.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
