using System.Linq;

namespace FlareHR.Battleship
{
    public class Board
    {
        private readonly Position[] _shipPositions;
        private readonly Position[] _attackPositions;

        private Board(Position[] shipPositions, Position[] attackPositions)
        {
            _shipPositions = shipPositions;
            _attackPositions = attackPositions;
        }

        public static Board Create()
        {
            return new Board(new Position[0], new Position[0]);
        }

        public (Board board, bool added) AddShip(Ship ship)
        {
            if (ship != null && !_shipPositions.Intersect(ship.OccupiedPositions).Any())
            {
                return (new Board(_shipPositions.Concat(ship.OccupiedPositions).ToArray(), _attackPositions), true);
            }

            return (this, false);
        }

        public (Board board, bool hit) AddAttack(Position attack) 
        {
            if (attack != null)
            {
                if (_shipPositions.Any(y => Equals(y, attack)))
                {
                    return (new Board(_shipPositions, _attackPositions.Concat(new[] {attack}).ToArray()), true);
                }

                return (new Board(_shipPositions, _attackPositions.Concat(new[] {attack}).ToArray()), false);
            }

            return (this, false);
        }

        public bool AreShipsAfloat()
        {
            return _shipPositions.Intersect(_attackPositions).Count() != _shipPositions.Length;
        }
    }
}
