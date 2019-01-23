using NUnit.Framework;

namespace FlareHR.Battleship.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Given_empty_board_When_check_if_ships_are_afloat_Then_result_is_false()
        {
            // Arrange
            var board = Board.Create();

            // Act
            var result = board.AreShipsAfloat();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_empty_board_When_ship_add_Then_board_has_ships_afloat()
        {
            // Arrange
            var board = Board.Create();

            // Act
            var (newBoard, added) = board.AddShip(Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5));

            // Assert
            Assert.That(added);
            Assert.That(newBoard.AreShipsAfloat());
        }

        [Test]
        public void Given_empty_board_When_ship_added_beyond_board_boundary_Then_ship_not_added_to_board()
        {
            // Arrange
            var board = Board.Create();

            // Act
            var (_, added) = board.AddShip(Ship.Create(Position.Create("G1"), Orientation.Horizontal, 5));

            // Assert
            Assert.False(added);
        }

        [Test]
        public void Given_empty_board_When_attack_added_Then_result_is_miss()
        {
            // Arrange
            var board = Board.Create();

            // Act
            var (_, hit) = board.AddAttack(Position.Create("A1"));

            // Assert
            Assert.False(hit);
        }

        [Test]
        public void Given_board_with_ship_When_attack_added_Then_result_is_hit()
        {
            // Arrange
            var board = GetBoardWithShip(Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5));

            // Act
            var (_, hit) = board.AddAttack(Position.Create("A1"));

            // Assert
            Assert.True(hit);
        }



        [Test]
        public void Given_board_with_ship_When_add_ship_in_non_overlapping_position_Then_the_ship_is_added()
        {
            // Arrange
            var board = GetBoardWithShip(Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5));

            // Act
            var (_, added) = board.AddShip(Ship.Create(Position.Create("A2"), Orientation.Vertical, 3));

            // Assert 
            Assert.True(added);
        }

        [Test]
        public void Given_board_with_ship_When_add_ship_in_overlapping_position_Then_the_ship_is_not_added()
        {
            // Arrange
            var board = GetBoardWithShip(Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5));

            // Act
            var (_, added) = board.AddShip(Ship.Create(Position.Create("A1"), Orientation.Vertical, 3));

            // Assert 
            Assert.False(added);
        }

        [Test]
        public void Given_board_with_ship_When_all_ship_positions_attacked_Then_no_ships_afloat()
        {
            // Arrange
            var board = GetBoardWithShip(Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5));

            // Act
            var (boardState1, _) = board.AddAttack(Position.Create("A1"));
            var (boardState2, _) = boardState1.AddAttack(Position.Create("B1"));
            var (boardState3, _) = boardState2.AddAttack(Position.Create("C1"));
            var (boardState4, _) = boardState3.AddAttack(Position.Create("D1"));
            var (boardState5, _) = boardState4.AddAttack(Position.Create("E1"));

            // Assert 
            Assert.False(boardState5.AreShipsAfloat());
        }

        [Test]
        public void
            Given_board_with_multiple_ships_When_all_ship_positions_attacked_with_hits_and_misses_Then_no_ships_afloat()
        {
            // Arrange
            var board = GetBoardWithShips(new[]
            {
                Ship.Create(Position.Create("A1"), Orientation.Horizontal, 5),
                Ship.Create(Position.Create("G6"), Orientation.Vertical, 3)
            });

            // Act
            var (boardState1, _) = board.AddAttack(Position.Create("A1")); //hit
            var (boardState2, _) = boardState1.AddAttack(Position.Create("B1")); //hit
            var (boardState3, _) = boardState2.AddAttack(Position.Create("C1")); //hit
            var (boardState4, _) = boardState3.AddAttack(Position.Create("D1")); //hit
            var (boardState5, _) = boardState4.AddAttack(Position.Create("E1")); //hit
            var (boardState6, _) = boardState5.AddAttack(Position.Create("G6")); //hit 
            var (boardState7, _) = boardState6.AddAttack(Position.Create("F6")); //miss
            var (boardState8, _) = boardState7.AddAttack(Position.Create("G7")); //hit
            var (boardState9, _) = boardState8.AddAttack(Position.Create("G8")); //hit

            // Assert 
            Assert.False(boardState9.AreShipsAfloat());
        }

        [Test]
        public void Given_board_with_ship_When_attack_same_location_Then_ship_not_sunk()
        {
            // Arrange
            var board = GetBoardWithShip(Ship.Create(Position.Create("B2"), Orientation.Vertical, 2));

            var (boardState1, _) = board.AddAttack(Position.Create("B2"));
            var (boardState2, _) = boardState1.AddAttack(Position.Create("B2"));

            // Assert 
            Assert.True(boardState2.AreShipsAfloat());
        }

        private Board GetBoardWithShip(Ship ship)
        {
            var board = Board.Create();
            var (newBoard, _) = board.AddShip(ship);

            return newBoard;
        }

        private Board GetBoardWithShips(Ship[] ships)
        {
            var board = Board.Create();
            foreach (var ship in ships)
            {
                var (newBoard, _) = board.AddShip(ship);
                board = newBoard;
            }

            return board;
        }
    }
}