namespace Battleship.Core
{
    public readonly struct Location
    {
        public int ColumnIndex { get; init; }
        public int RowIndex { get; init; }
        public string Cooridnates { get { return $"{(char)(ColumnIndex + 65)}{RowIndex + 1}"; } }

        public Location(int rowIndex, int columnIndex)
        {
            if(columnIndex < 0 || columnIndex > 25)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Valid board location index is a number in the range 0 to 25.");
            }

            if (rowIndex < 0 || rowIndex > 25)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Valid board location index is a number in the range 0 to 25.");
            }

            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }

        public Location(string literalCoordinates)
        {
            int parsedColumnNumber;
            var columnIsValidNumber = int.TryParse(literalCoordinates.Substring(1), out parsedColumnNumber);


            var tmpColIndex = literalCoordinates[0] - 'A';
            var tmpRowIndex = parsedColumnNumber - 1;

            if (!columnIsValidNumber || tmpColIndex < 0 || tmpColIndex > 25 || tmpRowIndex < 0 || tmpRowIndex > 25)
            {
                throw new ArgumentOutOfRangeException(nameof(literalCoordinates), "Valid board coordination has format '[A-Z][1-25].'");
            }

            RowIndex = tmpRowIndex;
            ColumnIndex = tmpColIndex;
        }
    }
}
