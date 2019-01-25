using DwarfMine.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
{
    public class ObjectLayer
    {
        private List<Models.Object> _objects;
        private List<Models.Object> _objectsVisible;
        private List<Models.Object> _objectsToAdd;
        private bool _askClear;

        public ObjectLayer()
        {
            _objects = new List<Models.Object>();
            _objectsToAdd = new List<Models.Object>();
        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            if(_objectsToAdd.Any())
            {
                _objects.AddRange(_objectsToAdd);
                _objectsToAdd.Clear();
            }

            if(_askClear)
            {
                _objects.Clear();
                _askClear = false;
            }

            var rectangle = camera.BoundingRectangle;
            rectangle.Inflate(Constants.TILE_WIDTH * 0.5f, Constants.TILE_HEIGHT * 0.5f);

            _objectsVisible = _objects.Where(x => rectangle.Contains(new Point(x.X, x.Y))).ToList();

            foreach (var obj in _objectsVisible)
            {
                obj.Update(time);
            }
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            _objectsVisible.Sort(delegate (Models.Object a, Models.Object b)
            {
                if (a.Y == b.Y)
                {
                    return a.X < b.X ? -1 : 1;
                }
                return a.Y < b.Y ? -1 : 1;
            });

            foreach (var obj in _objectsVisible)
            {
                obj.Draw(spriteBatch, time);
            }
        }

        public void AddObject(Models.Object obj)
        {
            _objectsToAdd.Add(obj);
        }

        public void RemoveAll()
        {
            _askClear = true;
        }
    }
}
