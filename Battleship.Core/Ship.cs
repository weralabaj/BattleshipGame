
namespace Battleship.Core
{
    public class Ship
    {
        private List<Cell> _occupiedCells { get; set; }
        public int Length { get; init; }
        public Ship(int length)
        {
            _occupiedCells = new List<Cell>();
            Length = length;
        }

        public void OccupyCells(List<Cell> cells)
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
