using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components
{
    public enum Facing
    {
        Left, Right, Up, Down
    }

    public enum State
    {
        Idle,
        Kicking,
        Punching,
        Jumping,
        Falling,
        Walking,
        Cool
    }

    public class Player
    {
        public Facing Facing { get; set; } = Facing.Down;
        public State State { get; set; }
        public bool IsAttacking => State == State.Kicking || State == State.Punching;
    }
}
