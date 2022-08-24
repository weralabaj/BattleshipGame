using Battleship.Core;
using NUnit.Framework;
using System;

namespace Battleship.Tests
{
    [TestFixture]
    public class PlacingShipsTests
    {
        [Test]
        public void DestroyerOccupiesExpectedCellsAfterPlacing()
        {
            var gameBoard = new Game();
            var battleship = new Ship(5);
            gameBoard.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);
            
            var destroyer = new Ship(4);
            gameBoard.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);

            var shotResult1 = gameBoard.Shoot("A1");
            var shotResult2 = gameBoard.Shoot("A2");
            var shotResult3 = gameBoard.Shoot("A3");
            var shotResult4 = gameBoard.Shoot("A4");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Sink, shotResult4);
        }

        [Test]
        public void BattleshipOccupiesExpectedCellsAfterPlacing()
        {
            var gameBoard = new Game();
            var destroyer = new Ship(4);
            gameBoard.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);

            var battleship = new Ship(5);
            gameBoard.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);

            var shotResult1 = gameBoard.Shoot("F10");
            var shotResult2 = gameBoard.Shoot("G10");
            var shotResult3 = gameBoard.Shoot("H10");
            var shotResult4 = gameBoard.Shoot("I10");
            var shotResult5 = gameBoard.Shoot("J10");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
            Assert.AreEqual(ShotResult.Sink, shotResult5);
        }

        [TestCase("G10", ShipOrientation.Vertical)]
        [TestCase("J6", ShipOrientation.Horizontal)]
        [TestCase("F27", ShipOrientation.Vertical)]
        public void CannotPlaceShipOutsideTheBoard(string startCoordinates, ShipOrientation shipOrientation)
        {
            var ocean = new Game();
            var battleship = new Ship(5);

            Assert.Throws<ArgumentOutOfRangeException>(() => ocean.PlaceShip(battleship, startCoordinates, shipOrientation));
        }

        [TestCase("A1", ShipOrientation.Vertical)]
        [TestCase("A1", ShipOrientation.Horizontal)]
        [TestCase("A2", ShipOrientation.Vertical)]
        [TestCase("A2", ShipOrientation.Horizontal)]
        [TestCase("A3", ShipOrientation.Vertical)]
        [TestCase("A3", ShipOrientation.Horizontal)]
        [TestCase("A4", ShipOrientation.Vertical)]
        [TestCase("A4", ShipOrientation.Horizontal)]
        [TestCase("F10", ShipOrientation.Vertical)]
        [TestCase("F10", ShipOrientation.Horizontal)]
        [TestCase("G10", ShipOrientation.Vertical)]
        [TestCase("G10", ShipOrientation.Horizontal)]
        [TestCase("H10", ShipOrientation.Vertical)]
        [TestCase("H10", ShipOrientation.Horizontal)]
        [TestCase("I10", ShipOrientation.Vertical)]
        [TestCase("I10", ShipOrientation.Horizontal)]
        [TestCase("J10", ShipOrientation.Vertical)]
        [TestCase("J10", ShipOrientation.Horizontal)]
        public void CannotPlaceShipOverAnotherShip(string startCoordinates, ShipOrientation shipOrientation)
        {
            var ocean = new Game();
            var destroyer = new Ship(4);
            ocean.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);
            var battleship = new Ship(5);
            ocean.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);

            var anotherDestroyer = new Ship(4);


            Assert.Throws<ArgumentOutOfRangeException>(() => ocean.PlaceShip(anotherDestroyer, startCoordinates, shipOrientation));
        }
    }
}
