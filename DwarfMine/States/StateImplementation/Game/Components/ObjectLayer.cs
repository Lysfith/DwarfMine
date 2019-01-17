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
        private List<Object> _objects;
        private List<Object> _objectsVisible;
        private List<Object> _objectsToAdd;
        private bool _askClear;

        public ObjectLayer()
        {
            _objects = new List<Object>();
            _objectsToAdd = new List<Object>();
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

            
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            var rectangle = camera.BoundingRectangle;
            rectangle.Inflate(Constants.TILE_WIDTH * 0.5f, Constants.TILE_HEIGHT * 0.5f);

            _objectsVisible = _objects.Where(x => rectangle.Contains(new Point(x.X, x.Y))).ToList();

            _objectsVisible.Sort(delegate (Object a, Object b)
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

        public void AddObject(Object obj)
        {
            _objectsToAdd.Add(obj);
        }

        public void RemoveAll()
        {
            _askClear = true;
        }
    }
}
