using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SudokuSolver
{
    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    class GraphNode<T> where T : IEquatable<T>
    {

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the GraphNode class.
        /// </summary>
        public GraphNode() : this(default(T)) {}

        /// <summary>
        /// Initialize a new instance of the GraphNode class.
        /// </summary>
        /// <param name="data">The value to be stored in the node.</param>
        public GraphNode(T data)
        {
            Data = data;
            Neighbors = new Collection<GraphNode<T>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of this node instance.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Returns the set of neighbors for this graph node.
        /// </summary>
        public ICollection<GraphNode<T>> Neighbors { get; private set; }

        #endregion

    }
}
