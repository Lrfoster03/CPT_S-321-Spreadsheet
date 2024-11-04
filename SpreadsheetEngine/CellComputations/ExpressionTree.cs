// <copyright file="ExpressionTree.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    /// <summary>
    /// Our expression tree class for functions.
    /// </summary>
    public class ExpressionTree
    {
        private string expression = string.Empty;
        private List<string> expressionSplit = new List<string>();
        private string? variableName;
        private double variableValue;

        private ShuntingYard sy = new ShuntingYard();

        private OperatorNode? root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">Accepts a string expression for our equation (EX: 3+3+4).</param>
        public ExpressionTree(string expression)
        {
            this.SetExpression(expression);
        }

        /// <summary>
        /// Adds a new node to our tree.
        /// </summary>
        /// <param name="input">Accepts a string array of an individual expression from our postfix equation.</param>
        public void AssembleTree(List<string> input)
        {
            OperatorNodeFactory opf = new OperatorNodeFactory();
            this.root = opf.ConstructNodes(input);
        }

        /// <summary>
        /// Our GetExpression method returning our current expression.
        /// </summary>
        /// <returns>Returns our current expresison. </returns>
        public string GetExpression()
        {
            return this.expression;
        }

        /// <summary>
        /// Our SetExpression accepting a new expression string.
        /// </summary>
        /// <param name="expression">Accpets a string expression to replace current expression.</param>
        public void SetExpression(string expression)
        {
            // Not a blank expression.
            if (expression != string.Empty)
            {
                this.expression = expression;
                this.expressionSplit = this.sy.ConvertToRPN(expression);
                this.AssembleTree(this.expressionSplit);
            }
        }

        /// <summary>
        /// Set our variables in our expression tree to a variableValue.
        /// </summary>
        /// <param name="variableName">Accepts a string variableName to replace with a value.</param>
        /// <param name="variableValue">Accepts a double variableValue to assign to our variable name.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableName = variableName;
            this.variableValue = variableValue;
            this.root = this.root is null ? throw new NullReferenceException("Root is null") : (OperatorNode)this.ParseVariable(this.root);
        }

        /// <summary>
        /// Our Evaluate method that evaluates all our OperatorNodes.
        /// </summary>
        /// <returns>Returns our evaluated values. </returns>
        /// Have nodes run recursively over all vals of expressionSplit and create new nodes based on old ones.
        public double Evaluate()
        {
            if (this.root == null)
            {
                return 0;
            }
            else
            {
                return this.root.Evaluate();
            }
        }

        /// <summary>
        /// Parses over all our nodes to find VariableNode.
        /// </summary>
        /// <returns>Our updated node value with our updated variable values. </returns>
        private Node ParseVariable(Node root)
        {
            if (root.GetType() == typeof(OperatorNode))
            {
                OperatorNode temp = (OperatorNode)root ?? throw new NullReferenceException("Root is null");
                temp.Left = this.ParseVariable(temp.Left ?? throw new NullReferenceException("Left is null"));
                temp.Right = this.ParseVariable(temp.Right ?? throw new NullReferenceException("Right is null"));
                return temp;
            }
            else if (root.GetType() == typeof(VariableNode))
            {
                VariableNode temp = (VariableNode)root;
                if (temp.VarName == this.variableName)
                {
                    temp.VariableValue = this.variableValue;
                }

                return temp;
            }

            // Must be a constant node
            else
            {
                return root;
            }
        }
    }
}
