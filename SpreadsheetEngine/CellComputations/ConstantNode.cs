// <copyright file="ConstantNode.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantNode"/> class.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        public ConstantNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">Our double value for our constructor.</param>
        public ConstantNode(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets our value double for a constant.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// We evaluate our node value.
        /// </summary>
        /// <returns>Returns the evaluated value of our node.</returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
