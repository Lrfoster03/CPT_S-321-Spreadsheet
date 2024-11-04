// <copyright file="ShuntingYard.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Computations
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Our Shunting Yard algorithm for turning expressions into Postfix notation and greatly helping with order of operations.
    /// </summary>
    public class ShuntingYard
    {
        // Assigns precedence to each operator.
        private Dictionary<string, int> operators = new Dictionary<string, int>
        {
            { "+", 2 },
            { "-", 2 },
            { "*", 3 },
            { "/", 3 },
        };

        /// <summary>
        /// Converts our traditional expression into a Postfix notation. (Heavily inspired from Wikipedia's Description of ShuntingYard).
        /// </summary>
        /// <param name="input">Accepts a string expression for our equation (EX: 3+3+4).</param>
        /// <returns>Returns a Reverse Polish Notation (Postfix Notation) of our origional equation (3 3 + 4 +).</returns>
        public List<string> ConvertToRPN(string input)
        {
            Stack<string> operatorStack = new Stack<string>();
            List<string> outputList = new List<string>();

            foreach (string token in this.Tokenize(input))
            {
                // If its a constant
                if (double.TryParse(token, out double number))
                {
                    outputList.Add(token);
                }

                // If its an operator.
                else if (this.operators.ContainsKey(token))
                {
                    while (operatorStack.Count > 0 && this.operators.ContainsKey(operatorStack.Peek()) && this.operators[token] <= this.operators[operatorStack.Peek()])
                    {
                        outputList.Add(operatorStack.Pop());
                    }

                    operatorStack.Push(token);
                }

                // Checking parenthesis.
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        outputList.Add(operatorStack.Pop());
                    }

                    // Discard the opening parenthesis
                    operatorStack.Pop();
                }

                // Otherwise, it's a variable.
                else
                {
                    outputList.Add(token);
                }
            }

            // Adds remaining operators to our result.
            while (operatorStack.Count > 0)
            {
                outputList.Add(operatorStack.Pop());
            }

            return outputList;
        }

        /// <summary>
        /// Splits the input expression into individual tokens, which can be operators, parentheses, variables, or numerical values.
        /// </summary>
        /// <param name="input">Accepts a string expression for our equation (EX: 3+3+4).</param>
        /// <returns>IEnumerable string object to help us parse through our tokens. </returns>
        public ArrayList Tokenize(string input)
        {
            string buildVars = string.Empty;
            ArrayList result = new ArrayList();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' || input[i] == ')' || this.operators.ContainsKey(input.Substring(i, 1)))
                {
                    // Add expression
                    if (buildVars != string.Empty)
                    {
                        result.Add(buildVars);
                    }

                    // Add operator OR Parenthesis.
                    result.Add(input.Substring(i, 1));
                    buildVars = string.Empty;
                }

                // Otherwise, we put together our expression.
                else
                {
                    buildVars += input[i];

                    if ((i + 1) == input.Length)
                    {
                        result.Add(buildVars);
                    }
                }
            }

            return result;
        }
    }
}