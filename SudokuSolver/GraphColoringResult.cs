using System;

namespace SudokuSolver
{
    /// <summary>
    /// Represents a coloring of the nodes of a graph.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    class GraphColoringResult<T> where T : IEquatable<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Vertex that is being colored.
        /// </summary>
        public GraphNode<T> Vertex { get; set; }

        /// <summary>
        /// Gets or sets the color of the graph node.
        /// </summary>
        public int Color { get; set; }

        #endregion
    }
}