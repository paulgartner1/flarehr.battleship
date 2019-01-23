using NUnit.Framework;

namespace FlareHR.Battleship.Tests
{
    [TestFixture]
    public class ShipTests
    {
        [Test]
        public void Given_ship_size_within_board_size_When_create_Then_the_ship_is_created()
        {
            // Arrange
            var shipSize = 7;

            // Act
            var ship = Ship.Create(Position.Create("A1"), Orientation.Horizontal, shipSize);

            // Assert
            Assert.IsNotNull(ship);
        }

        [Test]
        public void Given_ship_size_beyond_board_size_When_create_Then_the_ship_is_not_created()
        {
            // Arrange
            var shipSize = 11;

            // Act
            var ship = Ship.Create(Position.Create("A1"), Orientation.Horizontal, shipSize);

            // Assert
            Assert.IsNull(ship);
        }

        [Test]
        public void Given_ship_position_beyond_board_size_When_create_Then_the_ship_is_not_created()
        {
            // Arrange
            var shipSize = 5;

            // Act
            var ship = Ship.Create(Position.Create("G7"), Orientation.Horizontal, shipSize);

            // Assert
            Assert.IsNull(ship);
        }

        [Test]
        public void Given_ship_position_beyond_board_size_When_create_Then_the_ship_is_not_created2()
        {
            // Arrange
            var shipSize = 5;

            // Act
            var ship = Ship.Create(Position.Create("G7"), Orientation.Vertical, shipSize);

            // Assert
            Assert.IsNull(ship);
        }
    }
}
