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
            var boardLocation = new Location(rowIndex, colIndex, 26);

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
            var boardLocation = new Location(literalCoordinates, 26);

            Assert.AreEqual(expRowIndex, boardLocation.ColumnIndex);
            Assert.AreEqual(expColIndex, boardLocation.RowIndex);
        }

        [TestCase("A27", 26)]
        [TestCase("a1", 26)]
        [TestCase("11", 26)]
        [TestCase("A0", 26)]
        [TestCase("A11", 10)]
        [TestCase("L0", 10)]
        public void EnforcesConstraintsOnBoardLocationIndexes(string literalCoordinates, int size)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() 
                => new Location(literalCoordinates, size));
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
