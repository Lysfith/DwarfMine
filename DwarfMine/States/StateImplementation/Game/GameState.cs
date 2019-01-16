using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game.Components;
using DwarfMine.States.StateImplementation.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace DwarfMine.States.StateImplementation.Game
{
    class GameState : IState
    {
        private SpriteFont _font;
        private World _world;
        private EntityFactory _entityFactory;
        private KeyboardListener _keyboardListener;
        private MouseListener _mouseListener;

        private Map _map;

        public GameState()
        {
            _font = AssetManager.Instance.MainFont;
        }

        public void Start()
        {
            _world = new WorldBuilder()
               //.AddSystem(new WorldSystem())
               //.AddSystem(new PlayerSystem())
               //.AddSystem(new EnemySystem())
               //.AddSystem(new RenderSystem(GraphicManager.Instance.SpriteBatch, GraphicManager.Instance.Camera))
               .Build();

            _entityFactory = new EntityFactory(_world);

            var camera = GraphicManager.Instance.Camera;

            camera.MinimumZoom = 0.5f;
            camera.MaximumZoom = 3f;

            camera.LookAt(Vector2.Zero);

            _keyboardListener = new KeyboardListener();
            _keyboardListener.KeyTyped += KeyTyped;
            _keyboardListener.KeyPressed += KeyPressed;
            _keyboardListener.KeyReleased += KeyReleased;

            _mouseListener = new MouseListener();
            _mouseListener.MouseClicked += MouseClicked;
            _mouseListener.MouseDoubleClicked += MouseDoubleClicked;
            _mouseListener.MouseDown += MouseDown;
            _mouseListener.MouseDrag += MouseDrag;
            _mouseListener.MouseDragEnd += MouseDragEnd;
            _mouseListener.MouseDragStart += MouseDragStart;
            _mouseListener.MouseMoved += MouseMoved;
            _mouseListener.MouseUp += MouseUp;
            _mouseListener.MouseWheelMoved += MouseWheelMoved;

            _map = new Map();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _map.CreateRegion(x, y);
                }
            }

            //_map.CreateRegion(0, 0);
        }

        public void End()
        {

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            _keyboardListener.Update(time);
            _mouseListener.Update(time);

            _world.Update(time);

            _map.Update(time, camera);
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            _world.Draw(time);

            _map.Draw(time, spriteBatch, camera);
        }

        #region Inputs
        private void KeyPressed(object sender, KeyboardEventArgs args)
        {

        }

        private void KeyReleased(object sender, KeyboardEventArgs args)
        {

        }

        private void KeyTyped(object sender, KeyboardEventArgs args)
        {

        }

        private void MouseClicked(object sender, MouseEventArgs args)
        {

        }

        private void MouseDoubleClicked(object sender, MouseEventArgs args)
        {

        }

        private void MouseDown(object sender, MouseEventArgs args)
        {

        }

        private void MouseDrag(object sender, MouseEventArgs args)
        {
            var distance = args.DistanceMoved;

            var camera = GraphicManager.Instance.Camera;

            camera.Move(-distance / camera.Zoom);
        }

        private void MouseDragEnd(object sender, MouseEventArgs args)
        {

        }

        private void MouseDragStart(object sender, MouseEventArgs args)
        {

        }

        private void MouseMoved(object sender, MouseEventArgs args)
        {

        }

        private void MouseUp(object sender, MouseEventArgs args)
        {

        }

        private void MouseWheelMoved(object sender, MouseEventArgs args)
        {
            var camera = GraphicManager.Instance.Camera;

            if (args.ScrollWheelDelta > 0)
            {
                float value = 1;

                if (camera.Zoom < 1)
                {
                    value = 0.1f;
                }

                camera.ZoomIn(value);
            }
            else
            {
                float value = 1;

                if(camera.Zoom <= 1)
                {
                    value = 0.1f;
                }

                camera.ZoomOut(value);
            }
        }
        #endregion
    }
}
