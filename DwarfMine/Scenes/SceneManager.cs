using Autofac;
using DwarfMine.Core;
using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Util;
using DwarfMine.States.StateImplementation.Game;
using DwarfMine.Utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DwarfMine.States
{
    public class SceneManager
    {
        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                }

                return _instance;
            }
        }

        private EnumScene? _currentScene;
        private Dictionary<EnumScene, IScene> _scenes;

        public SceneManager()
        {
            _scenes = new Dictionary<EnumScene, IScene>();
        }

        public void SetScene(EnumScene scene)
        {
            if (_currentScene.HasValue)
            {
                _scenes[_currentScene.Value].End();
            }

            bool sceneHasChanged = false;

            switch (scene)
            {
                //case EnumGameState.MainMenu:
                //    if (!m_gameState.HasValue
                //        || m_gameState.Value == EnumGameState.Pause)
                //    {
                //        m_gameStates.Clear();
                //        m_gameStates[gameState] = new MainMenuState();

                //        stateHasChanged = true;
                //    }
                //    break;
                case EnumScene.Game:
                    _scenes[scene] = new GameScene();
                    _scenes[scene].Start();
                    //if (m_gameState.HasValue
                    //    && (m_gameState.Value == EnumGameState.MainMenu
                    //    || m_gameState.Value == EnumGameState.Pause))
                    //{
                    //    if (m_gameState.Value == EnumGameState.MainMenu)
                    //    {
                            
                    //    }
                    //    else if (m_gameState.Value == EnumGameState.Pause)
                    //    {
                    //        m_gameStates[gameState].Resume();
                    //    }

                       sceneHasChanged = true;
                    //}
                    break;
                //case EnumGameState.Pause:
                //    if (m_gameState.HasValue
                //        && m_gameState.Value == EnumGameState.Game)
                //    {
                //        m_gameStates[m_gameState.Value].Pause();

                //        m_gameStates[gameState] = new PauseState();
                //        m_gameStates[gameState].Start();

                //        stateHasChanged = true;
                //    }
                //    break;
            }

            using (var scope = GameCore.Instance.CreateScope())
            {
                var logService = scope.Resolve<ILogService>();

                if (sceneHasChanged)
                {

#if DEBUG
                    logService.Info(
                        string.Format("L'état du jeu a été modifié ! ({0} => {1})",
                        _currentScene, scene));
#endif

                    this._currentScene = scene;
                }
                else
                {
#if DEBUG
                    logService.Info(
                        string.Format("L'état du jeu n'a pas pu être modifié ! ({0} => {1})",
                        _currentScene, scene));
#endif
                }
            }
        }

        public void Update(GameTime time, OrthographicCamera camera)
        {
            if (_currentScene.HasValue)
            {
                _scenes[_currentScene.Value].Update(time, camera);
            }
        }

        public void Draw(GameTime time, CustomSpriteBatch spritebatch, OrthographicCamera camera)
        {
            if (_currentScene.HasValue)
            {
                _scenes[_currentScene.Value].Draw(time, spritebatch, camera);
            }
        }
    }
}
