namespace Battleship.Core
{
    public class Cell
    {
        public Ship? Ship { get; set; }
        public bool HasBeenHit { get; private set; }
        public bool IsOccupied { get { return Ship != null; } }

        public void PlaceShip(Ship ship)
        {
            Ship = ship;
        }

        public ShotResult Shoot()
        {
            HasBeenHit = true;

            if (Ship != null)
            {
                return Ship.Shoot();
            }

            return ShotResult.Miss;
        }
    }
}
