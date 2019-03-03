using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Resource;
using DwarfMine.Interfaces.Util;
using DwarfMine.Scenes.SceneImplementation.Game;
using DwarfMine.Scenes.SceneImplementation.Game.Systems;
using DwarfMine.States.StateImplementation.Game.Layers;
using DwarfMine.States.StateImplementation.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Sprites;
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
        private AnimatedSprite _cellHighlight;
        private Point? _currentCellHovered;
        private Vector2 _currentCellScale;
        private float _currentCellTime;
        private int _currentCellSign = -1;

        //private List<Character> _characters;
        private World _world;
        private EntityFactory _entityFactory;

        public GameScene()
        {
            
        }

        public void Start()
        {
            using (var scope = GameCore.Instance.CreateScope())
            {
                var spriteService = scope.Resolve<ISpriteService>();
                var fontService = scope.Resolve<IFontService>();
                var globalService = scope.Resolve<IGlobalService>();

                _font = fontService.GetFont("PixelMaster-24");

                var camera = globalService.GetCamera();
                var spritebatch = globalService.GetSpriteBatch();

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
                    .AddSystem(new SpriteVisibilitySystem(camera))
                    .AddSystem(new SpriteRenderingSystem(spritebatch, camera))
                    .AddSystem(new MovementSystem())
                    .Build();

                _entityFactory = new EntityFactory(_world);

                //entityFactory.SpawnPlayer(100, 100);

                //_characters = new List<Character>();


                _cellHighlight = spriteService.GetAnimatedSprite((int)EnumSpriteAnimated.CURSOR);
                _cellHighlight.Play("default");
            }
            _currentCellScale = new Vector2(1, 1);
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

            //_currentCellTime += (float)time.ElapsedGameTime.TotalSeconds;
            //_currentCellScale += new Vector2(0.2f * (float)time.ElapsedGameTime.TotalSeconds, 0.2f * (float)time.ElapsedGameTime.TotalSeconds) * _currentCellSign;

            //if (_currentCellTime >= 1)
            //{
            //    _currentCellSign = _currentCellSign * -1;
            //    _currentCellTime = 0;
            //}

            _world.Update(time);

            _cellHighlight.Update(time);
        }

        public void Draw(GameTime time, CustomSpriteBatch spriteBatch, OrthographicCamera camera)
        {
            MapSystem.Instance.Draw(time, spriteBatch, camera);

            _world.Draw(time);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());

            if (_currentCellHovered != null)
            {
                spriteBatch.Draw(_cellHighlight, new Vector2(_currentCellHovered.Value.X, _currentCellHovered.Value.Y), 0, _currentCellScale);
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
            OrthographicCamera camera = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var globalService = scope.Resolve<IGlobalService>();

                camera = globalService.GetCamera();
            }

            if (args.Button == MonoGame.Extended.Input.MouseButton.Left)
            {
                var mousePositionScreen = args.Position;
                
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
            else if (args.Button == MonoGame.Extended.Input.MouseButton.Middle)
            {
                var mousePositionScreen = args.Position;

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
                OrthographicCamera camera = null;

                using (var scope = GameCore.Instance.CreateScope())
                {
                    var globalService = scope.Resolve<IGlobalService>();

                    camera = globalService.GetCamera();
                }

                var distance = args.DistanceMoved;

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
            OrthographicCamera camera = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var globalService = scope.Resolve<IGlobalService>();

                camera = globalService.GetCamera();
            }

            var mousePositionScreen = args.Position;
            
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

        private void MouseUp(object sender, MouseEventArgs args)
        {

        }

        private void MouseWheelMoved(object sender, MouseEventArgs args)
        {
            OrthographicCamera camera = null;

            using (var scope = GameCore.Instance.CreateScope())
            {
                var globalService = scope.Resolve<IGlobalService>();

                camera = globalService.GetCamera();
            }

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

                if (camera.Zoom <= 1)
                {
                    value = 0.1f;
                }

                camera.ZoomOut(value);
            }
        }
        #endregion
    }
}
