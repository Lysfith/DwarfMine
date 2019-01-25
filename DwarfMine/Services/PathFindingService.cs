using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Services
{
    public static class PathFindingService
    {
        private const int MAX_VALUE = 65535;
        private const byte MAX_COST = 255;

        public static Vector2[,] ComputeFlow(byte[,] costs, int destinationX, int destinationY)
        {
            var values = new int[costs.GetLength(0), costs.GetLength(1)];
            var flows = new Vector2[costs.GetLength(0), costs.GetLength(1)];

            ResetValues(ref values);

            ComputeIntegrationField(ref costs, ref values, destinationX, destinationY);

            ComputeFlowField(ref values, ref flows, destinationX, destinationY);

            return flows;
        }

        private static void ResetValues(ref int[,] values)
        {
            for (int y = 0; y < values.GetLength(1); y++)
            {
                for (int x = 0; x < values.GetLength(0); x++)
                {
                    values[x, y] = 65535;
                }
            }
        }

        private static void ComputeIntegrationField(ref byte[,] costs, ref int[,] values, int destinationX, int destinationY)
        {
            var listOpen = new Queue<Point>();

            values[destinationX, destinationY] = 0;

            listOpen.Enqueue(new Point(destinationX, destinationY));

            while (listOpen.Any())
            {
                var currentPosition = listOpen.Dequeue();
                var currentValue = values[currentPosition.X, currentPosition.Y];
                var currentCost = costs[currentPosition.X, currentPosition.Y];

                var neighborsN = currentPosition + new Point(0, -1);
                var neighborsS = currentPosition + new Point(0, 1);
                var neighborsW = currentPosition + new Point(-1, 0);
                var neighborsE = currentPosition + new Point(1, 0);

                //unsigned int endNodeCost = getValueByIndex(currentID)                         
                //+ getCostField()-&gt;getCostByIndex(neighbors[i]);
                if (neighborsN.Y >= 0)
                {
                    var costN = costs[neighborsN.X, neighborsN.Y];

                    if (costN < MAX_COST)
                    {
                        var valueN = currentValue + (currentCost > costN ? currentCost : costN);

                        if (valueN < values[neighborsN.X, neighborsN.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsN.X && p.Y == neighborsN.Y))
                            {
                                listOpen.Enqueue(neighborsN);
                            }

                            values[neighborsN.X, neighborsN.Y] = valueN;
                        }
                    }
                }
                if (neighborsS.Y < costs.GetLength(1))
                {
                    var costS = costs[neighborsS.X, neighborsS.Y];

                    if (costS < MAX_COST)
                    {
                        var valueS = currentValue + (currentCost > costS ? currentCost : costS);

                        if (valueS < values[neighborsS.X, neighborsS.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsS.X && p.Y == neighborsS.Y))
                            {
                                listOpen.Enqueue(neighborsS);
                            }

                            values[neighborsS.X, neighborsS.Y] = valueS;
                        }
                    }
                }
                if (neighborsW.X >= 0)
                {
                    var costW = costs[neighborsW.X, neighborsW.Y];

                    if (costW < MAX_COST)
                    {
                        var valueW = currentValue + (currentCost > costW ? currentCost : costW);

                        if (valueW < values[neighborsW.X, neighborsW.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsW.X && p.Y == neighborsW.Y))
                            {
                                listOpen.Enqueue(neighborsW);
                            }

                            values[neighborsW.X, neighborsW.Y] = valueW;
                        }
                    }
                }
                if (neighborsE.X < costs.GetLength(0))
                {
                    var costE = costs[neighborsE.X, neighborsE.Y];

                    if (costE < MAX_COST)
                    {
                        var valueE = currentValue + (currentCost > costE ? currentCost : costE);

                        if (valueE < values[neighborsE.X, neighborsE.Y])
                        {
                            if (!listOpen.Any(p => p.X == neighborsE.X && p.Y == neighborsE.Y))
                            {
                                listOpen.Enqueue(neighborsE);
                            }

                            values[neighborsE.X, neighborsE.Y] = valueE;
                        }
                    }
                }
            }
        }

        private static void ComputeFlowField(ref int[,] values, ref Vector2[,] flows, int destinationX, int destinationY)
        {
            for (int y = 0; y < values.GetLength(1); y++)
            {
                for (int x = 0; x < values.GetLength(0); x++)
                {
                    flows[x, y] = ComputeFlowDirection(ref values, ref flows, x, y);
                }
            }

        }

        private static Vector2 ComputeFlowDirection(ref int[,] values, ref Vector2[,] flows, int x, int y)
        {
            var currentPosition = new Point(x, y);
            var neighborsN = currentPosition + new Point(0, -1);
            var neighborsS = currentPosition + new Point(0, 1);
            var neighborsW = currentPosition + new Point(-1, 0);
            var neighborsE = currentPosition + new Point(1, 0);
            var neighborsNE = currentPosition + new Point(1, -1);
            var neighborsNW = currentPosition + new Point(-1, -1);
            var neighborsSE = currentPosition + new Point(1, 1);
            var neighborsSW = currentPosition + new Point(-1, 1);

            var currentValue = values[x, y];

            if (currentValue == 65535 || currentValue == 0)
            {
                return Vector2.Zero;
            }

            var minValue = int.MaxValue;
            Point? minNeighbors = null;

            if (neighborsN.Y >= 0)
            {
                var value = values[neighborsN.X, neighborsN.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsN;
                }
            }
            if (neighborsS.Y < values.GetLength(1))
            {
                var value = values[neighborsS.X, neighborsS.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsS;
                }
            }
            if (neighborsW.X >= 0)
            {
                var value = values[neighborsW.X, neighborsW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsW;
                }
            }
            if (neighborsE.X < values.GetLength(0))
            {
                var value = values[neighborsE.X, neighborsE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsE;
                }
            }
            if (neighborsNW.X >= 0 && neighborsNW.Y >= 0)
            {
                var value = values[neighborsNW.X, neighborsNW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsNW;
                }
            }
            if (neighborsNE.X < values.GetLength(0) && neighborsNE.Y >= 0)
            {
                var value = values[neighborsNE.X, neighborsNE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsNE;
                }
            }
            if (neighborsSW.X >= 0 && neighborsSW.Y < values.GetLength(1))
            {
                var value = values[neighborsSW.X, neighborsSW.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsSW;
                }
            }
            if (neighborsSE.X < values.GetLength(0) && neighborsSE.Y < values.GetLength(1))
            {
                var value = values[neighborsSE.X, neighborsSE.Y];

                if (value < minValue)
                {
                    minValue = value;
                    minNeighbors = neighborsSE;
                }
            }

            if (minNeighbors != null)
            {
                var v = (minNeighbors.Value.ToVector2() - currentPosition.ToVector2());
                v.Normalize();
                return v;
            }

            return Vector2.Zero;
        }
    }
}
