
namespace Battleship.Core
{
    public enum ShotResult { Hit, Miss, Sink }
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

        public void PlaceShips()
        {
            var destroyer = new Ship();
            destroyer.OccupyCells(new[] { grid[0, 0], grid[1, 0], grid[2, 0], grid[3, 0] });
            var battleship = new Ship();
            battleship.OccupyCells(new[] { grid[9, 5], grid[9, 6], grid[9, 7], grid[9, 8], grid[9,9] });
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
