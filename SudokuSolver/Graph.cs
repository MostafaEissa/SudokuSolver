using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SudokuSolver
{
    /// <summary>
    /// Represents a graph.  A graph is an arbitrary collection of GraphNode instances.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    /// <remarks>
    /// This implementation is bases on the article by Scott Mitchell published on MSDN library.
    /// The article is entitled "An Extensive Examination of Data Structures Using C# 2.0" and
    /// was last updated January 2005.  
    /// <see cref="http://msdn.microsoft.com/en-us/library/ms379574%28VS.80%29.aspx"/>
    /// </remarks>
    class Graph<T> where T : IEquatable<T>
    {
        #region fields

        private readonly IList<GraphNode<T>> _nodeSet;

        #endregion
        #region Constructors

        /// <summary>
        /// Initialize a new Graph instance.
        /// </summary>
        public Graph() : this(null)   {}


        /// <summary>
        /// Initialize a new Graph instance.
        /// </summary>
        /// <param name="nodeSet">The initial set of nodes in the graph.</param>
        public Graph(IList<GraphNode<T>> nodeSet)
        {
            if (nodeSet == null)
                _nodeSet = new Collection<GraphNode<T>>();
            else
                _nodeSet = nodeSet;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the set of nodes in the graph.
        /// </summary>
        public IList<GraphNode<T>> Nodes
        {
            get { return _nodeSet; }
        }

        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int Count
        {
            get { return _nodeSet.Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="node">The GraphNode instance to add.</param>
        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            _nodeSet.Add(node);
        }

        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="value">The value to add to the graph</param>
        public void AddNode(T value)
        {
            _nodeSet.Add(new GraphNode<T>(value));
        }


        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The value of one of the GraphNodes that is joined by the edge.</param>
        public void AddUndirectedEdge(T from, T to)
        {
            var fromNode = _nodeSet.FindByValue(from);
            var toNode = _nodeSet.FindByValue(to);

            //if we did not find the nodes we cannot add them.
            if (fromNode == null || toNode == null) return;

            if (fromNode.Neighbors.Contains(toNode) || toNode.Neighbors.Contains(fromNode)) return;

            fromNode.Neighbors.Add(toNode);

            toNode.Neighbors.Add(fromNode);
        }

        /// <summary>
        /// Adds an undirected edge from one GraphNode to another.
        /// </summary>
        /// <param name="fromNode">One of the GraphNodes that is joined by the edge.</param>
        /// <param name="toNode">One of the GraphNodes that is joined by the edge.</param>
        public void AddUndirectedEdge(GraphNode<T> fromNode, GraphNode<T> toNode)
        {
            if (fromNode == null || toNode == null) return;

            if (fromNode.Neighbors.Contains(toNode) || toNode.Neighbors.Contains(fromNode)) return;

            fromNode.Neighbors.Add(toNode);

            toNode.Neighbors.Add(fromNode);
        }

        /// <summary>
        /// Clears out the contents of the Graph.
        /// </summary>
        public void Clear()
        {
            _nodeSet.Clear();
        }

        /// <summary>
        /// Returns a Boolean, indicating if a particular value exists within the graph.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>True if the value exist in the graph; false otherwise.</returns>
        public bool Contains(T value)
        {
            return _nodeSet.FindByValue(value) != null;
        }

        /// <summary>
        /// Attempts to remove a node from a graph.
        /// </summary>
        /// <param name="value">The value that is to be removed from the graph.</param>
        /// <returns>True if the corresponding node was found, and removed; false if the value was not
        /// present in the graph.</returns>
        /// <remarks>This method removes the GraphNode instance, and all edges leading to or from the
        /// GraphNode.</remarks>
        public bool Remove(T value)
        {
            // first remove the node from the nodeset
            GraphNode<T> nodeToRemove = _nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            _nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (var node in _nodeSet)
            {
                // remove the reference to the node.
                node.Neighbors.Remove(nodeToRemove);
            }

            return true;
        }

        /// <summary>
        /// Attempts to remove a node from a graph.
        /// </summary>
        /// <param name="node">The node that is to be removed from the graph.</param>
        /// <returns>True if the corresponding node was found, and removed; false if the value was not
        /// present in the graph.</returns>
        /// <remarks>This method removes the GraphNode instance, and all edges leading to or from the
        /// GraphNode.</remarks>
        public bool Remove(GraphNode<T> node)
        {
            return Remove(node.Data);
        }
        #endregion
    }
}
