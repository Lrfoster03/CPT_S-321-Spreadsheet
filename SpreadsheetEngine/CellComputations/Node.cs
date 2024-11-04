// <copyright file="Node.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
        }

        /// <summary>
        /// We evaluate our node value.
        /// </summary>
        /// <returns>Returns the evaluated value of our node.</returns>
        public virtual double Evaluate()
        {
            return 0;
        }
    }
}
