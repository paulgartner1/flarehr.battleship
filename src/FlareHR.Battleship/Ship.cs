using System;
using System.Collections.Generic;
using System.Linq;

namespace FlareHR.Battleship
{
    public class Ship
    {
        public Position[] OccupiedPositions { get; }

        private Ship(Position[] occupiedPositions)
        {
            OccupiedPositions = occupiedPositions;
        }

        public static Ship Create(Position bowPosition, Orientation orientation, int shipLength)
        {
            IEnumerable<Position> CalculateOccupiedPositions()
            {
                var currentX = bowPosition.X;
                var currentY = bowPosition.Y;

                for (int i = 0; i < shipLength; i++)
                {
                    yield return Position.Create(currentX, currentY);

                    if (orientation == Orientation.Horizontal)
                    {
                        currentX++;
                    }
                    else
                    {
                        currentY++;
                    }
                }
            }

            try
            {
                if (shipLength <= 0)
                {
                    return null;
                }

                var occupiedPositions = CalculateOccupiedPositions().ToArray();
                return new Ship(occupiedPositions);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}
