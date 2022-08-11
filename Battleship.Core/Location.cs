namespace Battleship.Core
{
    public readonly struct Location
    {
        public int ColumnIndex { get; init; }
        public int RowIndex { get; init; }
        public string Cooridnates { get { return $"{(char)(ColumnIndex + 65)}{RowIndex + 1}"; } }

        public Location(int rowIndex, int columnIndex, int maxSize = 10)
        {
            if(columnIndex < 0 || columnIndex > maxSize - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"Valid board location index is a number in the range 0 to {maxSize}.");
            }

            if (rowIndex < 0 || rowIndex > maxSize - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Valid board location index is a number in the range 0 to {maxSize}.");
            }

            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }

        public Location(string literalCoordinates, int maxSize = 10)
        {
            int parsedColumnNumber;
            var columnIsValidNumber = int.TryParse(literalCoordinates.Substring(1), out parsedColumnNumber);


            var tmpColIndex = literalCoordinates[0] - 'A';
            var tmpRowIndex = parsedColumnNumber - 1;

            if (!columnIsValidNumber || tmpColIndex < 0 || tmpColIndex > maxSize - 1 || tmpRowIndex < 0 || tmpRowIndex > maxSize - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(literalCoordinates), $"Valid board coordination has format '[A-Z][1-{maxSize}].'");
            }

            RowIndex = tmpRowIndex;
            ColumnIndex = tmpColIndex;
        }
    }
}
