using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Util
{
    public interface IFlowFieldService
    {
        Vector2[,] ComputeFlow(byte[,] costs, int destinationX, int destinationY);
    }
}
