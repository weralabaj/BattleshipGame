using Battleship.Core;
using NUnit.Framework;

namespace Battleship.Tests
{
    [TestFixture]
    public class GameTests
    {
        /* With seed 1000 the pseudo-random placement is the following:
         * Battlehsip: starting coordinates: 'E2', orientation: 'Vertial'
         * Destroyer: starting coordinates: 'G4', orientation: 'Vertial'
         * Destroyer: starting coordinates: 'G8', orientation: 'Horizontal'
         */

        [Test]
        public void GameOverIfAllShipsAreSunk()
        {
            var gameBoard = new Game();
            gameBoard.StartNewGame(1000);

            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("H3"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("H4"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("H5"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("H6"));
            Assert.AreEqual(ShotResult.Sink, gameBoard.Shoot("H7"));

            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("G4"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("G5"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("G6"));
            Assert.AreEqual(ShotResult.Sink, gameBoard.Shoot("G7"));

            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("G8"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("H8"));
            Assert.AreEqual(ShotResult.Hit, gameBoard.Shoot("I8"));
            Assert.AreEqual(ShotResult.GameOver, gameBoard.Shoot("J8"));
        }
    }
}
