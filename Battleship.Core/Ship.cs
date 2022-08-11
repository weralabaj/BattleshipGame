
namespace Battleship.Core
{
    public class Ship
    {
        private List<Cell> _occupiedCells { get; set; }
        public Ship()
        {
            _occupiedCells = new List<Cell>();
        }

        public void OccupyCells(Cell[] cells)
        {
            foreach (var cell in cells)
            {
                _occupiedCells.Add(cell);
                cell.PlaceShip(this);
            }
        }

        public bool IsSunk()
        {
            return _occupiedCells.All(c => c.HasBeenHit == true);
        }
    }
}
