using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SudokuSolver
{
    /// <summary>
    /// A set of helper functions to perform graph coloring.
    /// </summary>
    static class GraphHelpers
    {
        /// <summary>
        /// Searches the NodeList for a Node containing a particular value.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
        /// <param name="collection">An ICollection&lt;GraphNode&lt;&lt;T&gt;&gt; to search</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>The Node in the NodeList, if it exists; null otherwise.</returns>
        public static GraphNode<T> FindByValue<T>(this ICollection<GraphNode<T>> collection, T value)
            where T : IEquatable<T>
        {
            return collection.FirstOrDefault(n => n.Data.Equals(value));
        }

         /// <summary>
        /// Colors a graph using the Recursive-Largest-First algorithm.
        /// It returns a one possible coloring of the graph.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
        /// <param name="graph">The graph to be coloring.</param>
        /// <returns>One possible coloring of the graph.</returns>
        public static IList<GraphColoringResult<T>> Color<T>(this Graph<T> graph) where T : IEquatable<T>
         {
             IList<GraphColoringResult<T>> nodeSet = new List<GraphColoringResult<T>>();

            int colorNumber = 1; //number of used colors

             int numberOfColoredNodes = 0;

             while (numberOfColoredNodes < graph.Count)
             {
                 var max = -1;
                 var index = -1;

                 for (int i = 0; i < graph.Count; i++)
                 {
                     if (!Colored(graph.Nodes[i], nodeSet))
                     {
                         var d = SaturatedDegree(graph.Nodes[i], nodeSet);
                         if (d > max)
                         {
                             max = d;
                             index = i;
                         }

                         else if (d == max)
                         {
                             if (Degree(graph.Nodes[i]) > Degree(graph.Nodes[index]))
                             {
                                 index = i;
                             }
                         }
                     }
                 }

                AssignColor(graph.Nodes[index], nodeSet,ref colorNumber);
                numberOfColoredNodes++;
                 
             }
            

             return nodeSet;
         }

        #region Helpers
      
        /// <summary>
        /// Assign a color to an uncolored node.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
        /// <param name="graphNode">The node to be colored.</param>
        /// <param name="nodeSet">The set of colored nodes.</param>
        /// <param name="colorNumber">The current available color.</param>
        private static void AssignColor<T>(GraphNode<T> graphNode, IList<GraphColoringResult<T>> nodeSet, ref int colorNumber) where T : IEquatable<T>
        {
            var colors = nodeSet.Where(n => graphNode.Neighbors.Contains(n.Vertex)).GroupBy(n => n.Color).Select(g => g.Key).ToList();

            if (colors.Count == colorNumber) //all colors are used
            {
                colorNumber++;
                nodeSet.Add(new GraphColoringResult<T> { Vertex = graphNode, Color = colorNumber });
            }
            else //there is an unused color
            {
                var usedColors = Enumerable.Range(1, colorNumber);
                var colorNum = usedColors.Where(c => !colors.Contains(c)).OrderByDescending(c => nodeSet.Count(n => n.Color == c)).First();
                nodeSet.Add(new GraphColoringResult<T> { Vertex = graphNode, Color = colorNum });
            }
        }

        /// <summary>
        /// Finds the number of adjacent differently colored nodes.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
        /// <param name="graphNode">The node that we will find the saturated degree for.</param>
        /// <param name="nodeSet">The list of colored nodes.</param>
        /// <returns>The number of adjacent differently colored nodes.</returns>
        private static int SaturatedDegree<T>(GraphNode<T> graphNode, IEnumerable<GraphColoringResult<T>> nodeSet) where T : IEquatable<T>
        {
            return nodeSet.Where(n => graphNode.Neighbors.Contains(n.Vertex)).GroupBy(n => n.Color).Count();
        }

        /// <summary>
        /// Determine wether this node has been colored.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
        /// <param name="graphNode">The node under test.</param>
        /// <param name="nodeSet">The set of colored nodes.</param>
        /// <returns>True if the node has been colored, false otherwise.</returns>
        private static bool Colored<T>(GraphNode<T> graphNode, IEnumerable<GraphColoringResult<T>> nodeSet) where T : IEquatable<T>
        {
            return nodeSet.Any(n => n.Vertex.Data.Equals(graphNode.Data));
        }


        /// <summary>
        /// Finds the number of neighbors for a specific node.
        /// </summary>
        /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
        /// <param name="vertex">The vertex to find the number of neighbors for.</param>
        /// <returns>The number of neighbors for a specific GraphNode instance.</returns>
        private static int Degree<T>(GraphNode<T> vertex) where T : IEquatable<T>
        {
            return vertex.Neighbors.Count;
        }

        #endregion
    }
}