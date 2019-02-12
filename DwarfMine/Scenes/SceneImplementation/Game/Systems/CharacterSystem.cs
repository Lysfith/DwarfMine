using DwarfMine.States.StateImplementation.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Character> _charactersSelected;

        public CharacterSystem()
        {
            _characters = new List<Character>();
            _charactersSelected = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            _characters.Add(character);

            MapSystem.Instance.AddCharacter(character);
        }

        public Character GetCharacter(int xWorld, int yWorld)
        {
            return _characters.Where(x => x.X == xWorld && x.Y == yWorld).FirstOrDefault();
        }

        public void SelectCharacter(int xWorld, int yWorld)
        {
            var c = GetCharacter(xWorld, yWorld);

            if (c != null && !_charactersSelected.Contains(c))
            {
                _charactersSelected.Add(c);
            }
        }

        public IEnumerable<Character> GetSelectedCharacters()
        {
            return _charactersSelected;
        }
    }
}
