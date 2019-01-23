using System;
using System.Text.RegularExpressions;

namespace FlareHR.Battleship
{
    public class Position
    {
        private static readonly Regex ValidationRegex = new Regex(@"^[A-J](10|\d)$", RegexOptions.IgnoreCase);

        public char X { get; }
        public int Y { get; }

        public Position(char x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position Create(char x, int y)
        {
            return $"{x}{y}";
        }

        public static implicit operator Position(string position)
        {
            if (ValidationRegex.IsMatch(position))
            {
                var x = position[0];
                var y = int.Parse(position.Substring(1));

                return new Position(x, y);
            }

            throw new ArgumentOutOfRangeException(nameof(position), position, "Position must be between A1 and J10");
        }

        public override bool Equals(object obj)
        {
            if (obj is Position objPosition)
            {
                return objPosition.X == X && objPosition.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y;
            }
        }
    }
}
