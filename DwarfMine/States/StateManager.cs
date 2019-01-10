using DwarfMine.Graphics;
using DwarfMine.Managers;
using DwarfMine.States.StateImplementation.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DwarfMine.States
{
    public class StateManager
    {
        private static StateManager _instance;
        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                }

                return _instance;
            }
        }

        private EnumGameState? _currentGameState;
        private Dictionary<EnumGameState, IState> _gameStates;

        public StateManager()
        {
            _gameStates = new Dictionary<EnumGameState, IState>();
        }

        public void SetGameState(EnumGameState gameState)
        {
            if (_currentGameState.HasValue)
            {
                _gameStates[_currentGameState.Value].End();
            }

            bool stateHasChanged = false;

            switch (gameState)
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
                case EnumGameState.Game:
                    _gameStates[gameState] = new GameState();
                    _gameStates[gameState].Start();
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

                       stateHasChanged = true;
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


            if (stateHasChanged)
            {
                DebugGame.Log(
                    nameof(StateManager),
                    nameof(SetGameState),
                    string.Format("L'état du jeu a été modifié ! ({0} => {1})",
                    _currentGameState, gameState));

                this._currentGameState = gameState;
            }
            else
            {
                DebugGame.Log(
                    nameof(StateManager),
                    nameof(SetGameState),
                    string.Format("L'état du jeu n'a pas pu être modifié ! ({0} => {1})",
                    _currentGameState, gameState));
            }
        }

        public void Update(double time)
        {
            if (_currentGameState.HasValue)
            {
                _gameStates[_currentGameState.Value].Update(time);
            }
        }

        public void Draw(CustomSpriteBatch spritebatch)
        {
            if (_currentGameState.HasValue)
            {
                _gameStates[_currentGameState.Value].Draw(spritebatch);
            }
        }
    }
}
