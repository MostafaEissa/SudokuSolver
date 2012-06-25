using System;

namespace SudokuSolver
{
    /// <summary>
    /// Represents a cell in a sudoku puzzle.
    /// </summary>
    class SudokuCell : IEquatable<SudokuCell>
    {
        #region fields

        private int _row;
        private int _column;
        private int? _value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SudokuCell class.
        /// </summary>
        /// <param name="row">The row of the cell.</param>
        /// <param name="column">The column of the cell.</param>
        /// <param name="value">The value of the cell.</param>
        public SudokuCell(int row, int column, int? value)
        {
            Row = row;
            Column = column;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the SudokuCell class. The cell has no assigned value.
        /// </summary>
        /// <param name="row">The row of the cell.</param>
        /// <param name="column">The column of the cell.</param>
        public SudokuCell(int row, int column) : this(row, column, null) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cell row.
        /// </summary>
        public int Row
        {
            get { return _row; }
            set
            {
                if (value < 1 || value > 9)
                    throw new ArgumentOutOfRangeException("value", "The row value must be between 1 and 9");

                _row = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the cell column.
        /// </summary>
        public int Column
        {
            get { return _column; }
            set
            {
                if (value < 1 || value > 9)
                    throw new ArgumentOutOfRangeException("value", "The column must be between 1 and 9");

                _column  = value;
            }
        }

        /// <summary>
        /// Gets or sets the cell value. Null indicates an empty cell.
        /// </summary>
        public int? Value
        {
            get { return _value; }
            set
            {
                if (value.HasValue)
                {
                    if (value < 1 || value > 9)
                        throw new ArgumentOutOfRangeException("value", "The cell value must be between 1 and 9.");
                }
                _value = value;
            }
        }

        #endregion

        /// <summary>
        /// Determines whether the specified Object is equal to the current Object.
        /// </summary>
        /// <param name="other">The Object to compare with the current Object. </param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public bool Equals(SudokuCell other)
        {
            if (other == null) return false;

            return Row == other.Row
                   && Column == other.Column
                   && Value == other.Value;
        }
    }
}
