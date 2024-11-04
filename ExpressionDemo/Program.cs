// <copyright file="ExpressionTree.cs" company="Logan Foster">
// 11754587
// </copyright>

using SpreadsheetEngine;
using System.Collections;
using System.Linq.Expressions;

public class ExpressionDemo
{
    public static void Main(String[] args)
    {
        string? input = "";
        SpreadsheetEngine.ExpressionTree et = new SpreadsheetEngine.ExpressionTree("");
        while (input != "4")
        {
            Console.WriteLine("Menu (current expression=\"" + et.GetExpression() + "\")");
            Console.WriteLine(" 1 = Enter a new expression");
            Console.WriteLine(" 2 = Set a variable value");
            Console.WriteLine(" 3 = Evaluate Tree");
            Console.WriteLine(" 4 = Quit");

            // Reads user input.
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter new expression: ");
                    string? expression = Console.ReadLine();
                    et.SetExpression(expression);
                    break;
                case "2":
                    Console.Write("Enter variable name: ");
                    string? varName = Console.ReadLine();
                    Console.Write("Enter variable value: ");
                    double varVal = Convert.ToDouble(Console.ReadLine());
                    et.SetVariable(varName, varVal);
                    break;
                case "3":
                    Console.WriteLine(et.Evaluate());
                    break;
            }
        }
    }
}