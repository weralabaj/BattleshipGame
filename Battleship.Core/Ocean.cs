
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
            var startLocation = new Location(startingCoordinates, Size); 

            if(!CanPlaceShip(ship.Length, startingCoordinates, shipOrientation))
            {
                throw new ArgumentOutOfRangeException("The ship cannot be placed in the specified way.");
            }

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

        private bool CanPlaceShip(int shipLength, string startingCoordinates, ShipOrientation shipOrientation)
        {
            var startLocation = new Location(startingCoordinates, Size);

            switch (shipOrientation)
            {
                case ShipOrientation.Vertical:
                    {
                        if (startLocation.RowIndex + shipLength > Size)
                        {
                            return false;
                        }

                        for (int i = 0; i < shipLength; i++)
                        {
                            if (grid[startLocation.RowIndex + i, startLocation.ColumnIndex].IsOccupied)
                                return false;
                        }

                        return true;
                    }
                case ShipOrientation.Horizontal:
                    {
                        if (startLocation.ColumnIndex + shipLength > Size)
                        {
                            return false;
                        }

                        for (int i = 0; i < shipLength; i++)
                        {
                            if (grid[startLocation.RowIndex, startLocation.ColumnIndex + i].IsOccupied)
                                return false;
                        }

                        return true;
                    }
            }

            return false;
        }

        public ShotResult Shoot(string coordinates)
        {
            var location = new Location(coordinates, Size);

            var cell = grid[location.RowIndex, location.ColumnIndex];

            return cell.Shoot();
        }
    }
}
