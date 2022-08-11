using Battleship.Core;
using NUnit.Framework;
using System;

namespace Battleship.Tests
{
    [TestFixture]
    public class SinkingShipsTests
    {
        private Ocean _ocean;

        [SetUp]
        public void SetUp()
        {
            _ocean = new Ocean();
            var destroyer = new Ship(4);
            _ocean.PlaceShip(destroyer, "A1", ShipOrientation.Vertical);
            var battleship = new Ship(5);
            _ocean.PlaceShip(battleship, "F10", ShipOrientation.Horizontal);
        }

        [TestCase("11")]
        [TestCase("Z10")]
        [TestCase("A11")]
        public void CannotShootOutsideTheBoard(string location)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { _ocean.Shoot(location); });
        }

        [TestCase("B1")]
        [TestCase("A10")]
        [TestCase("J9")]
        public void WhenEmptyLocationShotThenItIsMiss(string location)
        {
            var result = _ocean.Shoot(location);

            Assert.AreEqual(ShotResult.Miss, result);
        }

        [Test]
        public void DestroyerIsSunkOn4thHit()
        {
            var shotResult1 = _ocean.Shoot("A1");
            var shotResult2 = _ocean.Shoot("A2");
            var shotResult3 = _ocean.Shoot("A3");
            var shotResult4 = _ocean.Shoot("A4");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Sink, shotResult4);
        }

        [Test]
        public void DestroyerIsNotSunkIfHittingIt4TimesInTheSameLocation()
        {
            var shotResult1 = _ocean.Shoot("A1");
            var shotResult2 = _ocean.Shoot("A1");
            var shotResult3 = _ocean.Shoot("A1");
            var shotResult4 = _ocean.Shoot("A1");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
        }

        [Test]
        public void BattleshipIsSunkOn5thHit()
        {
            var shotResult1 = _ocean.Shoot("F10");
            var shotResult2 = _ocean.Shoot("G10");
            var shotResult3 = _ocean.Shoot("H10");
            var shotResult4 = _ocean.Shoot("I10");
            var shotResult5 = _ocean.Shoot("J10");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
            Assert.AreEqual(ShotResult.Sink, shotResult5);
        }
    }
}
