// <copyright file="VariableNode.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableNode"/> class.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        public VariableNode()
        {
            this.VarName = string.Empty;
        }

        /// <summary>
        /// Gets or sets our variable value.
        /// </summary>
        public double VariableValue { get; set; }

        /// <summary>
        /// Gets or sets our variable name.
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// We evaluate our node value.
        /// </summary>
        /// <returns>Returns the evaluated value of our node.</returns>
        public override double Evaluate()
        {
            return this.VariableValue;
        }
    }
}
