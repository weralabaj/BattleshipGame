
namespace Battleship.Core
{
    public enum ShotResult { Hit, Miss, Sink }
    public enum ShipOrientation { Vertical, Horizontal }
    public class Ocean
    {
        public int Size { get; private set; }
        private readonly Cell[,] grid;

        public Ocean()
        {
            Size = 10;

            grid = new Cell[Size, Size];
            for (int rowIndex = 0; rowIndex < Size; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Size; colIndex++)
                {
                    grid[rowIndex, colIndex] = new Cell();
                }
            }
        }

        public void PlaceShip(Ship ship, string startingCoordinates, ShipOrientation shipOrientation)
        {
            var cells = new List<Cell>();
            var startLocation = new Location(startingCoordinates); 

            switch (shipOrientation)
            {
                case ShipOrientation.Vertical:
                    {
                        for (int i = 0; i < ship.Length; i++)
                        {
                            cells.Add(grid[startLocation.RowIndex + i, startLocation.ColumnIndex]);
                        }
                    }
                    break;
                case ShipOrientation.Horizontal:
                    {
                        for (int i = 0; i < ship.Length; i++)
                        {
                            cells.Add(grid[startLocation.RowIndex, startLocation.ColumnIndex + i]);
                        }
                    }
                    break;
            }

            ship.OccupyCells(cells);
        }

        public ShotResult Shoot(string coordinates)
        {
            var location = new Location(coordinates);

            if(location.RowIndex >= Size || location.ColumnIndex >= Size)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinates), "The provided coordinates are out of bound of this board.");
            }

            var cell = grid[location.RowIndex, location.ColumnIndex];

            return cell.Shoot();
        }
    }
}
