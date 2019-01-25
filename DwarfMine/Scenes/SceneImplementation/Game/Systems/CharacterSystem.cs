using DwarfMine.States.StateImplementation.Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Systems
{
    public class CharacterSystem
    {
        private static CharacterSystem _instance;

        public static CharacterSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterSystem();
                }

                return _instance;
            }
        }

        private List<Character> _characters;

        public CharacterSystem()
        {
            _characters = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            _characters.Add(character);

            MapSystem.Instance.AddCharacter(character);
        }
    }
}
