// <copyright file="OperatorNode.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNode"/> class.
    /// </summary>
    internal class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="op">Our operator to use in our evaluation.</param>
        public OperatorNode(char op)
        {
            this.Op = op;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Gets or sets our operand for this specific operator Node.
        /// </summary>
        public char Op { get; set; }

        /// <summary>
        /// Gets or sets the node to the left of our operator node.
        /// </summary>
        public Node? Left { get; set; }

        /// <summary>
        /// Gets or sets the node to the right of our operator node.
        /// </summary>
        public Node? Right { get; set; }

        /// <summary>
        /// We evaluate our left and right nodes accordingly.
        /// </summary>
        /// <returns>Returns the evaluated value of our left and right nodes.</returns>
        public override double Evaluate()
        {
            if (this.Left == null || this.Right == null)
            {
                throw new InvalidOperationException("Left or Right node is null.");
            }

            if (this.Op == '+')
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
            else if (this.Op == '-')
            {
                return this.Left.Evaluate() - this.Right.Evaluate();
            }
            else if (this.Op == '*')
            {
                return this.Left.Evaluate() * this.Right.Evaluate();
            }

            // this.op == '/'.
            else
            {
                return this.Left.Evaluate() / this.Right.Evaluate();
            }
        }
    }
}
