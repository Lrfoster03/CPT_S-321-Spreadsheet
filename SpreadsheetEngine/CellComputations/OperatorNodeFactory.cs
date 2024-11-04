// <copyright file="OperatorNodeFactory.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    using System.Collections.Generic;

    /// <summary>
    /// Factory to create a new operator node.
    /// </summary>
    internal class OperatorNodeFactory
    {
        private string operators = "+-*/";

        /// <summary>
        /// Factory to create a new operator node.
        /// </summary>
        /// <param name="op">A character representation of an operator.</param>
        /// <returns>An operator node.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            return new OperatorNode(op);
        }

        /// <summary>
        /// Adds a new node to our tree.
        /// </summary>
        /// <param name="input">Accepts a string array of an individual expression from our postfix equation.</param>
        /// <returns>OperatornNode thats the root of our tree.</returns>
        public OperatorNode ConstructNodes(List<string> input)
        {
            Stack<Node> st = new Stack<Node>();
            Node t1, t2, temp;
            foreach (string s in input)
            {
                // If not an operator
                if (!this.operators.Contains(s))
                {
                    // Checking if we need a constant node.
                    double number;
                    if (double.TryParse(s, out number))
                    {
                        temp = new ConstantNode();
                        ((ConstantNode)temp).Value = number;
                    }
                    else
                    {
                        temp = new VariableNode();
                        ((VariableNode)temp).VarName = s;
                    }

                    st.Push(temp);
                }

                // Need an operator node.
                else
                {
                    temp = this.CreateOperatorNode(char.Parse(s));
                    t1 = st.Pop();
                    t2 = st.Pop();
                    ((OperatorNode)temp).Right = t1;
                    ((OperatorNode)temp).Left = t2;
                    st.Push(temp);
                }
            }

            return (OperatorNode)st.Pop();
        }
    }
}
