
namespace Battleship.Core
{
    public enum ShotResult { Hit, Miss, Sink }
    public enum ShipOrientation { Vertical, Horizontal }
    public class GameBoard
    {
        private readonly int _size;
        private readonly Cell[,] _grid;

        public GameBoard(int size = 10)
        {
            _size = size;

            _grid = new Cell[_size, _size];
            for (int rowIndex = 0; rowIndex < _size; rowIndex++)
            {
                for (int colIndex = 0; colIndex < _size; colIndex++)
                {
                    _grid[rowIndex, colIndex] = new Cell();
                }
            }
        }

        public void PlaceShip(Ship ship, string startingCoordinates, ShipOrientation shipOrientation)
        {
            var cells = new List<Cell>();
            var startLocation = new Location(startingCoordinates, _size); 

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
                            cells.Add(_grid[startLocation.RowIndex + i, startLocation.ColumnIndex]);
                        }
                    }
                    break;
                case ShipOrientation.Horizontal:
                    {
                        for (int i = 0; i < ship.Length; i++)
                        {
                            cells.Add(_grid[startLocation.RowIndex, startLocation.ColumnIndex + i]);
                        }
                    }
                    break;
            }

            ship.OccupyCells(cells);
        }

        private bool CanPlaceShip(int shipLength, string startingCoordinates, ShipOrientation shipOrientation)
        {
            var startLocation = new Location(startingCoordinates, _size);

            switch (shipOrientation)
            {
                case ShipOrientation.Vertical:
                    {
                        if (startLocation.RowIndex + shipLength > _size)
                        {
                            return false;
                        }

                        for (int i = 0; i < shipLength; i++)
                        {
                            if (_grid[startLocation.RowIndex + i, startLocation.ColumnIndex].IsOccupied)
                                return false;
                        }

                        return true;
                    }
                case ShipOrientation.Horizontal:
                    {
                        if (startLocation.ColumnIndex + shipLength > _size)
                        {
                            return false;
                        }

                        for (int i = 0; i < shipLength; i++)
                        {
                            if (_grid[startLocation.RowIndex, startLocation.ColumnIndex + i].IsOccupied)
                                return false;
                        }

                        return true;
                    }
            }

            return false;
        }

        public ShotResult Shoot(string coordinates)
        {
            var location = new Location(coordinates, _size);

            var cell = _grid[location.RowIndex, location.ColumnIndex];

            return cell.Shoot();
        }
    }
}
