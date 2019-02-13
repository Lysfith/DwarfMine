using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.Scenes.SceneImplementation.Game;
using DwarfMine.Scenes.SceneImplementation.Game.Systems;
using DwarfMine.States.StateImplementation.Game.Layers;
using DwarfMine.States.StateImplementation.Game.Models;
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
    class GameScene : IScene
    {
        private SpriteFont _font;
        private KeyboardListener _keyboardListener;
        private MouseListener _mouseListener;
        private PrimitiveRectangle _cellHighlight;
        private Point? _currentCellHovered;

        private List<Character> _characters;
        private World _world;
        private EntityFactory _entityFactory;

        public GameScene()
        {
            _font = AssetManager.Instance.MainFont;
        }

        public void Start()
        {
            var camera = GraphicManager.Instance.Camera;
            var spritebatch = GraphicManager.Instance.SpriteBatch;

            camera.MinimumZoom = 0.1f;
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

            MapSystem.Instance.Load(1, 1);

            

            _world = new WorldBuilder()
                .AddSystem(new SpriteRenderingSystem(spritebatch, camera))
                .AddSystem(new MovementSystem())
                .Build();

            _entityFactory = new EntityFactory(_world);

            //entityFactory.SpawnPlayer(100, 100);

            //_characters = new List<Character>();

            _cellHighlight = new PrimitiveRectangle(PrimitiveRectangle.Type.OUTLINE, Constants.TILE_WIDTH, Constants.TILE_HEIGHT, TextureManager.Instance.GetTexture("blank"), Color.Transparent, Color.Red, 1);
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

            MapSystem.Instance.Update(time, camera);

            _world.Update(time);
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            MapSystem.Instance.Draw(time, spriteBatch, camera);

            _world.Draw(time);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());

            if (_currentCellHovered != null)
            {
                _cellHighlight.Draw(spriteBatch, _currentCellHovered.Value.X - Constants.TILE_HALF_WIDTH, _currentCellHovered.Value.Y - Constants.TILE_HALF_HEIGHT);
            }

            spriteBatch.End();

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
            if(args.Button == MonoGame.Extended.Input.MouseButton.Left)
            {
                var mousePositionScreen = args.Position;
                var camera = GraphicManager.Instance.Camera;

                var mousePositionWorld = camera.ScreenToWorld(mousePositionScreen.X, mousePositionScreen.Y);

                //MapSystem.Instance.AddObject((int)mousePositionWorld.X, (int)mousePositionWorld.Y);

                var region = MapSystem.Instance.GetRegion((int)mousePositionWorld.X, (int)mousePositionWorld.Y);
                var cellIndex = region.GetCellIndexFromWorldPosition((int)mousePositionWorld.X, (int)mousePositionWorld.Y);
                var isFree = !region.GetCollision(cellIndex.X, cellIndex.Y);

                if (isFree)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        _entityFactory.SpawnPlayer((int)mousePositionWorld.X, (int)mousePositionWorld.Y);
                    }

                    //var character = new Models.Character((int)mousePositionWorld.X, (int)mousePositionWorld.Y);

                    //CharacterSystem.Instance.AddCharacter(character);

                    //_characters.Add(character);
                }
            }
            else if(args.Button == MonoGame.Extended.Input.MouseButton.Middle)
            {
                var mousePositionScreen = args.Position;
                var camera = GraphicManager.Instance.Camera;

                var mousePositionWorld = camera.ScreenToWorld(mousePositionScreen.X, mousePositionScreen.Y);

                MapSystem.Instance.Select((int)mousePositionWorld.X, (int)mousePositionWorld.Y);

                //foreach (var character in _characters)
                //{
                //    character.SetDestination(mousePositionWorld.ToPoint());
                //}
            }
        }

        private void MouseDoubleClicked(object sender, MouseEventArgs args)
        {

        }

        private void MouseDown(object sender, MouseEventArgs args)
        {

        }

        private void MouseDrag(object sender, MouseEventArgs args)
        {
            if (args.Button == MonoGame.Extended.Input.MouseButton.Right)
            {
                var distance = args.DistanceMoved;

                var camera = GraphicManager.Instance.Camera;

                camera.Move(-distance / camera.Zoom);
            }
        }

        private void MouseDragEnd(object sender, MouseEventArgs args)
        {

        }

        private void MouseDragStart(object sender, MouseEventArgs args)
        {

        }

        private void MouseMoved(object sender, MouseEventArgs args)
        {
            if (args.Button == MonoGame.Extended.Input.MouseButton.Right)
            {
                var mousePositionScreen = args.Position;
                var camera = GraphicManager.Instance.Camera;

                var mousePositionWorld = camera.ScreenToWorld(mousePositionScreen.X, mousePositionScreen.Y);

                var region = MapSystem.Instance.GetRegion((int)mousePositionWorld.X, (int)mousePositionWorld.Y);

                if (region != null)
                {
                    _currentCellHovered = region.GetCellPositionFromWorldPosition((int)mousePositionWorld.X, (int)mousePositionWorld.Y);
                }
                else
                {
                    _currentCellHovered = null;
                }
            }
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
