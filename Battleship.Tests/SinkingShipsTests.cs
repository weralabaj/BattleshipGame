using Battleship.Core;
using NUnit.Framework;
using System;

namespace Battleship.Tests
{
    [TestFixture]
    public class SinkingShipsTests
    {
        [TestCase("11")]
        [TestCase("Z10")]
        [TestCase("A11")]
        public void CannotShootOutsideTheBoard(string location)
        {
            var ocean = new Ocean();

            Assert.Throws<ArgumentOutOfRangeException>(() => { ocean.Shoot(location); });
        }

        [TestCase("A1")]
        [TestCase("A10")]
        [TestCase("J10")]
        public void WhenEmptyLocationShotThenItIsMiss(string location)
        {
            var ocean = new Ocean();
         
            var result = ocean.Shoot(location);

            Assert.AreEqual(ShotResult.Miss, result);
        }

        [Test]
        public void DestroyerIsSunkOn4thHit()
        {
            var ocean = new Ocean();

            ocean.PlaceShips();

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
        public void DestroyerIsNotSunkIfHittingIt4TimesInTheSameLocation()
        {
            var ocean = new Ocean();

            ocean.PlaceShips();

            var shotResult1 = ocean.Shoot("A1");
            var shotResult2 = ocean.Shoot("A1");
            var shotResult3 = ocean.Shoot("A1");
            var shotResult4 = ocean.Shoot("A1");

            Assert.AreEqual(ShotResult.Hit, shotResult1);
            Assert.AreEqual(ShotResult.Hit, shotResult2);
            Assert.AreEqual(ShotResult.Hit, shotResult3);
            Assert.AreEqual(ShotResult.Hit, shotResult4);
        }

        [Test]
        public void BattleshipIsSunkOn5thHit()
        {
            var ocean = new Ocean();

            ocean.PlaceShips();

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
