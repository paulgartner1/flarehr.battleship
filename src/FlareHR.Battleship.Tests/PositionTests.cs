using System;
using NUnit.Framework;

namespace FlareHR.Battleship.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Given_position_within_board_When_position_created_Then_position_is_created()
        {
            // Arrange
            var position = "c5";
           
            // Act
            var result = Position.Create(position);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Given_position_outsize_board_When_position_created_Then_exception_thrown()
        {
            // Arrange
            var position = "k5";

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Position.Create(position));
        }

        [Test]
        public void Given_position_outsize_board_When_position_created_Then_exception_thrown2()
        {
            // Arrange
            var position = "c12";

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Position.Create(position));
        }

        [Test]
        public void Given_position_outsize_board_When_position_created_Then_exception_thrown3()
        {
            // Arrange
            var position = "c0";

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Position.Create(position));
        }
    }
}
