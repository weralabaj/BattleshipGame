
namespace Battleship.Core
{
    public class Cell
    {
        public Ship? Ship { get; set; }
        public bool HasBeenHit { get; private set; }

        public void PlaceShip(Ship ship)
        {
            Ship = ship;
        }

        public ShotResult Shoot()
        {
            HasBeenHit = true;

            if (Ship != null)
            {
                if (Ship.IsSunk())
                {
                    return ShotResult.Sink;
                }

                return ShotResult.Hit;
            }

            return ShotResult.Miss;
        }
    }
}
