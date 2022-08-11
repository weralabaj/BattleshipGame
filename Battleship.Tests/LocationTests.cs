using Battleship.Core;
using NUnit.Framework;
using System;

namespace Battleship.Tests
{
    [TestFixture]
    public class LocationTests
    {

        [TestCase(0, 0, "A1")]
        [TestCase(9, 0, "A10")]
        [TestCase(0, 9, "J1")]
        [TestCase(1, 1, "B2")]
        [TestCase(9, 9, "J10")]
        [TestCase(10, 10, "K11")]
        [TestCase(25, 25, "Z26")]
        public void BoardLocationToLiteralCoordinates(int rowIndex, int colIndex, string expectedLiteralCoordinates)
        {
            var boardLocation = new Location(rowIndex, colIndex);

            Assert.AreEqual(expectedLiteralCoordinates, boardLocation.Cooridnates);
        }


        [TestCase(0, 0, "A1")]
        [TestCase(9, 0, "J1")]
        [TestCase(0, 9, "A10")]
        [TestCase(1, 1, "B2")]
        [TestCase(9, 9, "J10")]
        [TestCase(10, 10, "K11")]
        [TestCase(25, 25, "Z26")]
        public void LiteralCoordinatesToBoardLocation(int expRowIndex, int expColIndex, string literalCoordinates)
        {
            var boardLocation = new Location(literalCoordinates);

            Assert.AreEqual(expRowIndex, boardLocation.ColumnIndex);
            Assert.AreEqual(expColIndex, boardLocation.RowIndex);
        }

        [TestCase("A27")]
        [TestCase("a1")]
        [TestCase("11")]
        [TestCase("A0")]
        public void EnforcesConstraintsOnBoardLocationIndexes(string literalCoordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() 
                => new Location(literalCoordinates));
        }

        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(26, 0)]
        [TestCase(0, 26)]
        public void EnforcesConstraintsOnBoardLocationCoordinates(int rowIndex, int columnIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new Location(rowIndex, columnIndex));
        }
    }
}
