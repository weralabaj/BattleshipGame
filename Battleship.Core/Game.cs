
namespace Battleship.Core
{
    public enum ShotResult { Hit, Miss, Sink, GameOver }
    public enum ShipOrientation { Vertical, Horizontal }
    public class Game
    {
        private readonly int _size;
        private readonly Cell[,] _grid;
        private List<Ship> _ships = new List<Ship>();

        public Game(int size = 10)
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

        public void StartNewGame(int? randomSeed = null)
        {
            var battleship = new Ship(5);
            var destroyer1 = new Ship(4);
            var destroyer2 = new Ship(4);
;
            PlaceShipInRandomLocation(randomSeed, battleship);
            PlaceShipInRandomLocation(randomSeed, destroyer1);
            PlaceShipInRandomLocation(randomSeed, destroyer2);

            void PlaceShipInRandomLocation(int? randomSeed, Ship ship)
            {
                ShipOrientation randomOrientation;
                Location randomPosition;
                var internalSeed = randomSeed;

                GenerateRandomPlacement(internalSeed, out randomOrientation, out randomPosition);

                while (!CanPlaceShip(ship.Length, randomPosition.Cooridnates, randomOrientation))
                {
                    internalSeed += randomSeed;
                    GenerateRandomPlacement(internalSeed, out randomOrientation, out randomPosition);
                }

                PlaceShip(ship, randomPosition.Cooridnates, randomOrientation);
            }

            void GenerateRandomPlacement(int? randomSeed, out ShipOrientation randomOrientation, out Location randomPosition)
            {
                var random = randomSeed != null ? new Random(randomSeed.Value) : new Random();

                var orientationValues = Enum.GetValues(typeof(ShipOrientation));
                int index = random.Next(maxValue: orientationValues.Length);
                randomOrientation = (ShipOrientation)orientationValues.GetValue(index);
                randomPosition = new Location(random.Next(_size), random.Next(_size));
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
            _ships.Add(ship);
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

            var shotResult = cell.Shoot();

            if(_ships.All(s => s.IsSunk()))
            {
                return ShotResult.GameOver;
            }

            return shotResult;
        }
    }
}
