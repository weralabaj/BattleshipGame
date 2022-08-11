using Battleship.Core;
using NUnit.Framework;

namespace Battleship.Tests
{
    [TestFixture]
    public class PlacingShipsTests
    {
        [Test]
        public void DestroyerOccupiesExpectedCellsAfterPlacing()
        {
            var ocean = new Ocean();
            var destroyer = new Ship(4);

            ocean.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);

            var shotResult1 = ocean.Shoot("A1");
            var shotResult2 = ocean.Shoot("A2");
            var shotResult3 = ocean.Shoot("A3");
            var shotResult4 = ocean.Shoot("A4");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Sink, shotResult4);
        }

        [Test]
        public void BattleshipOccupiesExpectedCellsAfterPlacing()
        {
            var ocean = new Ocean();
            var battleship = new Ship(5);

            ocean.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);

            var shotResult1 = ocean.Shoot("F10");
            var shotResult2 = ocean.Shoot("G10");
            var shotResult3 = ocean.Shoot("H10");
            var shotResult4 = ocean.Shoot("I10");
            var shotResult5 = ocean.Shoot("J10");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
            Assert.AreEqual(ShotResult.Sink, shotResult5);
        }
    }
}
