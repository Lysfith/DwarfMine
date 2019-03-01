using DwarfMine.Models.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Components
{
    public class MovementDestination
    {
        public bool HasMoved { get; set; }
        public Point? Position { get; set; }
        public Queue<Point> Path { get; set; }
        public Job JobComputePath { get; set; }
    }
}
