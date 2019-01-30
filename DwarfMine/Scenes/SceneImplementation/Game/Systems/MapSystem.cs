using DwarfMine.Graphics;
using DwarfMine.States.StateImplementation.Game;
using DwarfMine.States.StateImplementation.Game.Models;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Systems
{
    public class MapSystem
    {
        private static MapSystem _instance;

        public static MapSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MapSystem();
                }

                return _instance;
            }
        }

        private readonly Map _map;

        private Random _random;

        public MapSystem()
        {
            _map = new Map();

            _random = new Random();
        }

        public void Load(int width, int height)
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    _map.CreateRegion(x, y);
                }
            }

            for (int i = 0; i < width * Constants.REGION_WIDTH * height * Constants.REGION_HEIGHT * 0.2f; i++)
            {
                int x = _random.Next(0, width * Constants.REGION_WIDTH * Constants.TILE_WIDTH);
                int y = _random.Next(0, height * Constants.REGION_HEIGHT * Constants.TILE_HEIGHT);

                AddObject(x, y);
            }

            _map.Loaded();
        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            _map.Update(time, camera);
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            _map.Draw(time, spriteBatch, camera);
        }

        public void Select(int x, int y)
        {
            var region = _map.GetRegion(x, y);

            if (region != null)
            {
                var cellIndex = region.GetCellIndexFromWorldPosition(x, y);

                region.Select(cellIndex.X, cellIndex.Y);
            }
        }

        public void AddObject(int x, int y)
        {
            var region = _map.GetRegion(x, y);

            if (region != null)
            {
                var cell = region.GetCellPositionFromWorldPosition(x, y);
                var cellIndex = region.GetCellIndexFromWorldPosition(x, y);
                x = cell.X;
                y = cell.Y;

                if (!region.GetCollision(cellIndex.X, cellIndex.Y))
                {
                    int type = _random.Next((int)EnumSprite.TREE_1, (int)EnumSprite.TREE_1 + 8);

                    if (type == 11)
                    {
                        type = 10;
                    }
                    _map.AddObject(new States.StateImplementation.Game.Models.Object(x, y, (EnumSprite)type));

                    region.SetCollision(cellIndex.X, cellIndex.Y, true);
                }
            }
        }

        public void AddCharacter(Character character)
        {
            _map.AddObject(character);
        }

        public Region GetRegion(int xWorld, int yWorld)
        {
            return _map.GetRegion(xWorld, yWorld);
        }
    }
}
