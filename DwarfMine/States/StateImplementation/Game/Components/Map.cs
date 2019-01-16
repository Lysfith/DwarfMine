using DwarfMine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
{
    public class Map
    {
        private Dictionary<string, RegionTile> _regions;
        private List<RegionTile> _activeRegions;

        public Map()
        {
            _regions = new Dictionary<string, RegionTile>();
            _activeRegions = new List<RegionTile>();
        }

        public void CreateRegion(int x, int y)
        {
            var region = new RegionTile(x, y);
            region.Apply();

            _regions.Add($"{x}_{y}", region);
        }

        public RegionTile GetRegion(int xWorld, int yWorld)
        {
            return _regions.Where(r => r.Value.Rectangle.Contains(xWorld, yWorld)).Select(r => r.Value).FirstOrDefault();
        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            var rectangle = camera.BoundingRectangle.ToRectangle();

            _activeRegions = _regions.Values.Where(r => r.Rectangle.Intersects(rectangle)).ToList();
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            if (_activeRegions.Any())
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());

                foreach (var region in _activeRegions)
                {
                    region.Draw(time, spriteBatch);
                }

                spriteBatch.End();
            }
        }
    }
}
