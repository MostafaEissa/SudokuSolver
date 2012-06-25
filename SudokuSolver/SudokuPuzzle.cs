using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    /// <summary>
    /// Represents a sudoku puzzle.
    /// </summary>
    internal class SudokuPuzzle : IEnumerable<SudokuCell>
    {
        #region fields

        private readonly SudokuCell[,] _cells = new SudokuCell[9,9];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SudokuPuzzle class.
        /// </summary>
        public SudokuPuzzle()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _cells[i, j] = new SudokuCell(i + 1, j + 1);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a cell in the puzzle.
        /// </summary>
        /// <param name="row">The row of the cell. A value between 1 and 9.</param>
        /// <param name="column">The column of the cell. A value between 1 and 9.</param>
        /// <returns></returns>
        public int? this[int row, int column]
        {
            get
            {
                if (row > 9 || row < 1)
                    throw new ArgumentOutOfRangeException("row", "The row value must be between 1 and 9");

                if (column > 9 || column < 1)
                    throw new ArgumentOutOfRangeException("column", "The column must be between 1 and 9");

                return _cells[row - 1, column - 1].Value;
            }

            set
            {
                if (row > 9 || row < 1)
                    throw new ArgumentOutOfRangeException("row", "The row value must be between 1 and 9");

                if (column > 9 || column < 1)
                    throw new ArgumentOutOfRangeException("column", "The column must be between 1 and 9");

                if (value.HasValue)
                {
                    if (value < 1 || value > 9)
                        throw new ArgumentOutOfRangeException("value", "The cell value must be between 1 and 9.");
                }

                _cells[row - 1, column - 1].Value = value;
            }
        }

        #endregion

        #region IEnumerable interface

        /// <summary>
        /// Returns an enumerator that allows for iterating through the cells of the puzzle.
        /// </summary>
        /// <returns>an enumerator that allows for iterating through the cells of the puzzle.</returns>
        public IEnumerator<SudokuCell> GetEnumerator()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    yield return _cells[i, j];
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that allows for iterating through the cells of the puzzle.
        /// </summary>
        /// <returns>an enumerator that allows for iterating through the cells of the puzzle.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Methods

        public bool Solve()
        {
            var graph = new Graph<SudokuCell>();

            //add nodes
            foreach (var cell in _cells)
            {
                graph.AddNode(cell);
            }

            //connect general contraints
            CreatePuzzleEdges(graph);

            //connect specific puzzle contraints
            CreateInstanceEdges(graph);

            //solve the puzzle
             var result = graph.Color();
            

            var success = !result.Any(r => r.Color > 9); //more than 9 colors used

              if (!success)
               return false;

            //apply the values
     
            var grouping = result
                .GroupBy(v => v.Color); //group cells by color


            foreach (var group in grouping)
            {
                var assignedDigit = group.FirstOrDefault(cell => cell.Vertex.Data.Value.HasValue);
                if (assignedDigit != null)
                {
                    foreach (var graphColoringResult in group)
                    {
                        graphColoringResult.Vertex.Data.Value = assignedDigit.Vertex.Data.Value.Value;
                    }
                }
            }


            return true;
        }

        /// <summary>
        /// Connect the necessary nodes together to apply contraints.
        /// </summary>
        /// <param name="graph">The graph representing the puzzle.</param>
        private static void CreatePuzzleEdges(Graph<SudokuCell> graph)
        {
            var cells = graph.Nodes.Select(n => n.Data).ToList();
            //add edges 
            var horizontalIndices = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8}; //cells in same row
            var verticalIndices = new int[] {0, 9, 18, 27, 36, 45, 54, 63, 72}; //cells in same column
            var diagonalIndices = new int[] {0, 1, 2, 9, 10, 11, 18, 19, 20}; //cells in same block

            int idx;

            for (int i = 0; i < 81; i++)
            {

                int r = cells[i].Row - 1; //subtract 1 as array are zero based
                int c = cells[i].Column - 1; //subtract 1 as array are zero based

                for (int j = 0; j < 9; j++)
                {
                    idx = horizontalIndices[j] + r*9;
                    if (idx != (r*9 + c)) //skip self
                        graph.AddUndirectedEdge(cells[i], cells[idx]);

                    idx = verticalIndices[j] + c;
                    if (idx != (r*9 + c)) //skip self
                        graph.AddUndirectedEdge(cells[i], cells[idx]);

                    idx = diagonalIndices[j] + ((r/3)*3)*9 + (c/3)*3;
                    if (idx != (r*9 + c)) //skip self
                        graph.AddUndirectedEdge(cells[i], cells[idx]);
                }
            }
        }

        /// <summary>
        /// Connect the necessary nodes together to apply contraints based on cell values.
        /// </summary>
        /// <param name="graph">The graph representing the puzzle.</param>
        private static void CreateInstanceEdges(Graph<SudokuCell> graph)
        {
            var cells = graph.Nodes.Select(n => n.Data).ToList();

            foreach (var sudokuCell in cells)
            {
                if (sudokuCell.Value.HasValue)
                {
                    var others = cells
                        .Where(c => c.Value.HasValue && c.Value.Value != sudokuCell.Value.Value);

                    foreach (var other in others)
                    {
                        graph.AddUndirectedEdge(sudokuCell, other);
                    }

  
                    var horizontalIndices = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8}; //cells in same row
                    var verticalIndices = new int[] {0, 9, 18, 27, 36, 45, 54, 63, 72}; //cells in same column
                    var diagonalIndices = new int[] {0, 1, 2, 9, 10, 11, 18, 19, 20}; //cells in same block

                    int idx;

                    var equals = cells
                        .Where(c => c.Value.HasValue
                                    && c.Value == sudokuCell.Value
                                    && c != sudokuCell)
                        .ToList();


                    foreach (var node in equals)
                    {

                        int r = node.Row - 1; //subtract 1 as array are zero based
                        int c = node.Column - 1; //subtract 1 as array are zero based

                        for (int j = 0; j < 9; j++)
                        {
                            idx = horizontalIndices[j] + r*9;
                            if (idx != (r*9 + c)) //skip self
                                graph.AddUndirectedEdge(sudokuCell, cells[idx]);

                            idx = verticalIndices[j] + c;
                            if (idx != (r*9 + c)) //skip self
                                graph.AddUndirectedEdge(sudokuCell, cells[idx]);

                            idx = diagonalIndices[j] + ((r/3)*3)*9 + (c/3)*3;
                            if (idx != (r*9 + c)) //skip self
                                graph.AddUndirectedEdge(sudokuCell, cells[idx]);
                        }
                    }
                }
            }   
        }

        /// <summary>
        /// Returns a string that represents the current puzzle.
        /// </summary>
        /// <returns>a String that represents the current puzzle.</returns>
        public override string ToString()
        {
            var strBuilder = new StringBuilder(264);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    strBuilder.AppendFormat("{0}", _cells[i, j].Value == null ? "*" : _cells[i, j].Value.ToString());
                    strBuilder.Append(' ');

                    if (j == 2 || j == 5)
                        strBuilder.Append("| ");
                }
                strBuilder.AppendLine();
                if (i == 2 || i == 5)
                {
                    strBuilder.Append("----------------------");
                    strBuilder.AppendLine();
                }
            }

            return strBuilder.ToString();
        }

        #endregion
    }
}
