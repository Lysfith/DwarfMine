using DwarfMine.Windows.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Windows.States
{
    public interface IState
    {
        void Start();
        void End();
        void Pause();
        void Resume();
        void Update(double time);
        void Draw(CustomSpriteBatch spritebatch);
    }
}
