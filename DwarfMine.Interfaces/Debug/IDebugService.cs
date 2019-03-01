using DwarfMine.Interfaces.Util;
using DwarfMine.Interfaces.Util.Base;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Debug
{
    public interface IDebugService : IDrawService
    {
        void ShowScreen();
        void HideScreen();
        
        void StartUpdate();
        void StopUpdate();
        void StartDraw();
        void StopDraw();
    }
}
