using DwarfMine.Graphics;
using DwarfMine.States.StateImplementation.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Models
{
    public class Map
    {
        private Dictionary<string, Region> _regions;
        private List<Region> _activeRegions;
        private ObjectLayer _objectLayer;

        public Map()
        {
            _regions = new Dictionary<string, Region>();
            _activeRegions = new List<Region>();
            _objectLayer = new ObjectLayer();
        }

        public void CreateRegion(int x, int y)
        {
            var region = new Region(x, y);

            _regions.Add($"{x}_{y}", region);
        }

        public Region GetRegion(int xWorld, int yWorld)
        {
            return _regions.Where(r => r.Value.Rectangle.Contains(xWorld, yWorld)).Select(r => r.Value).FirstOrDefault();
        }

        public void AddObject(Object obj)
        {
            _objectLayer.AddObject(obj);
        }

        public void Select(int x, int y)
        {
            var region = GetRegion(x, y);

            if(region != null)
            {
                var cellIndex = region.GetCellIndex(x, y);

                region.Select(cellIndex.X, cellIndex.Y);
            }
        }

        public void Loaded()
        {
            foreach (var region in _regions)
            {
                region.Value.Loaded();
            }
        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            var rectangle = camera.BoundingRectangle.ToRectangle();

            _activeRegions.Clear();

            _activeRegions = _regions.Values.Where(r => r.Rectangle.Intersects(rectangle)).ToList();

            foreach(var region in _regions)
            {
                region.Value.Update(time);

                if(region.Value.Rectangle.Intersects(rectangle))
                {
                    _activeRegions.Add(region.Value);

                    region.Value.SetVisible(true);
                }
                else
                {
                    region.Value.SetVisible(false);
                }
            }

            _objectLayer.Update(time, camera);
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            if (_activeRegions.Any())
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix(), blendState: BlendState.AlphaBlend);

                foreach (var region in _activeRegions)
                {
                    region.Draw(time, spriteBatch);
                }

                _objectLayer.Draw(time, spriteBatch, camera);

                spriteBatch.End();
            }
        }
    }
}
