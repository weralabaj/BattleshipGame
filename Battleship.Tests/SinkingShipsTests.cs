using Battleship.Core;
using NUnit.Framework;
using System;

namespace Battleship.Tests
{
    [TestFixture]
    public class SinkingShipsTests
    {
        private GameBoard _gameBoard;

        [SetUp]
        public void SetUp()
        {
            _gameBoard = new GameBoard();
            var destroyer = new Ship(4);
            _gameBoard.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);
            var battleship = new Ship(5);
            _gameBoard.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);
        }

        [TestCase("11")]
        [TestCase("Z10")]
        [TestCase("A11")]
        public void CannotShootOutsideTheBoard(string location)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { _gameBoard.Shoot(location); });
        }

        [TestCase("B1")]
        [TestCase("A10")]
        [TestCase("J9")]
        public void WhenEmptyLocationShotThenItIsMiss(string location)
        {
            var result = _gameBoard.Shoot(location);

            Assert.AreEqual(ShotResult.Miss, result);
        }

        [Test]
        public void DestroyerIsSunkOn4thHit()
        {
            var shotResult1 = _gameBoard.Shoot("A1");
            var shotResult2 = _gameBoard.Shoot("A2");
            var shotResult3 = _gameBoard.Shoot("A3");
            var shotResult4 = _gameBoard.Shoot("A4");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Sink, shotResult4);
        }

        [Test]
        public void BattleshipIsSunkOn5thHit()
        {
            var shotResult1 = _gameBoard.Shoot("F10");
            var shotResult2 = _gameBoard.Shoot("G10");
            var shotResult3 = _gameBoard.Shoot("H10");
            var shotResult4 = _gameBoard.Shoot("I10");
            var shotResult5 = _gameBoard.Shoot("J10");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
            Assert.AreEqual(ShotResult.Sink, shotResult5);
        }

        [Test]
        public void DestroyerIsNotSunkIfHittingIt4TimesInTheSameLocation()
        {
            _gameBoard.Shoot("A1");
            _gameBoard.Shoot("A1");
            _gameBoard.Shoot("A1");
            var lastShot = _gameBoard.Shoot("A1");

            Assert.AreEqual(ShotResult.Hit, lastShot);
        }

        [Test]
        public void BattleshipNotSunkIfNotAllCellsAreHit()
        {
            _gameBoard.Shoot("F10");
            _gameBoard.Shoot("F10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("H10");
            _gameBoard.Shoot("I10");
            var lastShot = _gameBoard.Shoot("I10");

            Assert.AreEqual(ShotResult.Hit, lastShot);
        }

        [Test]
        public void BattleshipSunkIfAllCellsAreHit()
        {
            _gameBoard.Shoot("F10");
            _gameBoard.Shoot("F10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("G10");
            _gameBoard.Shoot("H10");
            _gameBoard.Shoot("I10");
            _gameBoard.Shoot("I10");
            var lastShot = _gameBoard.Shoot("J10");

            Assert.AreEqual(ShotResult.Sink, lastShot);
        }
    }
}
