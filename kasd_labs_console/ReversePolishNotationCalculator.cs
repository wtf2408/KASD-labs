using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    class ReversePolishNotationCalculator
    {
        private static readonly Dictionary<string, int> operatorPriority = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "%", 2 },
            { "^", 3 },
            { "sqrt", 3 },
            { "abs", 3 },
            { "sign", 3 },
            { "sin", 3 },
            { "cos", 3 },
            { "tan", 3 },
            { "log", 3 },
            { "ln", 3 },
            { "min", 2 },
            { "max", 2 },
            { "div", 2 },
            { "mod", 2 },
            { "exp", 3 },
            { "truncate", 3 }
        };

        public static double Calc(string operation, double[] args)
        {
            switch (operation)
            {
                case "+":
                    return args[0] + args[1];
                case "-":
                    return args[0] - args[1];
                case "*":
                    return args[0] * args[1];
                case "/":
                    if (args[1] == 0) throw new DivideByZeroException("Division by zero");
                    return args[0] / args[1];
                case "^":
                    return Math.Pow(args[0], args[1]);
                case "sqrt":
                    return Math.Sqrt(args[0]);
                case "abs":
                    return Math.Abs(args[0]);
                case "sign":
                    return Math.Sign(args[0]);
                case "sin":
                    return Math.Sin(args[0]);
                case "cos":
                    return Math.Cos(args[0]);
                case "tan":
                    return Math.Tan(args[0]);
                case "log":
                    return Math.Log10(args[0]);
                case "ln":
                    return Math.Log(args[0]);
                case "min":
                    return Math.Min(args[0], args[1]);
                case "max":
                    return Math.Max(args[0], args[1]);
                case "div":
                    if (args[1] == 0) throw new DivideByZeroException("Division by zero.");
                    return Math.Floor(args[0] / args[1]);
                case "mod":
                    return args[0] % args[1];
                case "exp":
                    return Math.Exp(args[0]);
                case "truncate":
                    return Math.Truncate(args[0]);
                default:
                    throw new InvalidOperationException($"Unknown operator: {operation}");
            }
        }
        private static string[] ParseExpression(string expression)
        {
            string pattern = @"(\d+\.\d+|\d+|[a-zA-Z]+|[+\-*/^%()=,.])";
            Regex regex = new Regex(pattern);
            return regex.Matches(expression).Cast<Match>().Select(m => m.Value).ToArray();
        }
        public static MyStack<string> ToReversePolishNotation(string expression, Dictionary<string, double> variables)
        {
            MyStack<string> operatorStack = new MyStack<string>();
            MyStack<string> output = new MyStack<string>();
            string[] symbols = ParseExpression(expression);

            foreach (var symbol in symbols)
            {
                if (double.TryParse(symbol, out _))
                {
                    output.Push(symbol);
                }
                else if (variables.ContainsKey(symbol))
                {
                    output.Push(variables[symbol].ToString());
                }
                else if (symbol == "(")
                {
                    operatorStack.Push(symbol);
                }
                else if (symbol == ")")
                {
                    while (!operatorStack.IsEmpty() && operatorStack.Peek() != "(")
                    {
                        output.Push(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                else
                {
                    while (!operatorStack.IsEmpty() && operatorPriority.ContainsKey(operatorStack.Peek()) &&
                           operatorPriority[operatorStack.Peek()] >= operatorPriority[symbol])
                    {
                        output.Push(operatorStack.Pop());
                    }
                    operatorStack.Push(symbol);
                }
            }

            while (!operatorStack.IsEmpty())
            {
                output.Push(operatorStack.Pop());
            }

            return output;
        }

        public static double EvaluatePostfix(MyStack<string> postfixExpression)
        {
            MyStack<double> stack = new MyStack<double>();

            foreach (var token in postfixExpression)
            {
                if (double.TryParse(token, out double number))  // If it's a number
                {
                    stack.Push(number);
                }
                else  // If it's an operator
                {
                    if (operatorPriority.ContainsKey(token))
                    {
                        double[] operands;
                        if (token == "sqrt" || token == "abs" || token == "sign" || token == "sin" || token == "cos" || token == "tan" || token == "log" || token == "ln" || token == "exp" || token == "truncate")
                        {
                            operands = new double[] { stack.Pop() };
                        }
                        else
                        {
                            operands = new double[] { stack.Pop(), stack.Pop() };
                        }

                        double result = ApplyOperator(token, operands);
                        stack.Push(result);
                    }
                }
            }

            return stack.Pop();
        }

    }
}
