using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kasd_labs_console;


namespace kasd_labs_console
{
    internal class Program
    {
        private static string ReplaceVariablesWithValues(string expression, Dictionary<string, double> variables)
        {
            foreach (var variable in variables)
            {
                // Заменить все вхождения переменной на ее значение
                expression = expression.Replace(variable.Key, variable.Value.ToString());
            }
            return expression;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите математическое выражение:");
            string expression = Console.ReadLine();

            Console.WriteLine("Введите переменные (например, a=5 b=10), или нажмите Enter, чтобы пропустить:");
            string variablesInput = Console.ReadLine();

            var variables = new Dictionary<string, double>();

            if (!string.IsNullOrEmpty(variablesInput))
            {
                var variableAssignments = variablesInput.Split(' ');
                foreach (var assignment in variableAssignments)
                {
                    var parts = assignment.Split('=');
                    if (parts.Length == 2 && double.TryParse(parts[1], out double value))
                    {
                        variables[parts[0]] = value;
                    }
                    else
                    {
                        Console.WriteLine($"Некорректное присваивание переменной: {assignment}");
                    }
                }
            }
            expression = ReplaceVariablesWithValues(expression, variables);

            try
            {
                string postfixExpression = ExpressionCalculator.ConvertToPostfix(expression);
                Console.WriteLine("Обратная польская нотация:");
                Console.WriteLine(postfixExpression);

                double result = ExpressionCalculator.CalculatePostfix(postfixExpression);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public static class ExpressionCalculator
    {
        private static readonly Regex regularExp = new Regex(
            @"(\d+\.\d+|\d+|\+|\-|\*|\/|\^|\(|\)|sqrt|ln|log|sin|cos|tan|min|max|mod|abs)");

        private static readonly Dictionary<string, int> OperatorPriority = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 },
            { "sqrt", 4 },
            { "mod", 3 },
            { "min", 3 },
            { "max", 3 },
            { "log", 4 },
            { "ln", 4 },
            { "sin", 4 },
            { "cos", 4 },
            { "tan", 4 }
        };
        static bool IsUnaryOperator(string operation)
        {
            return Regex.IsMatch(operation, "^(sin|cos|tan|sqrt|ln|log|abs)$");
        }
        private static bool IsNumber(string token)
        {
            return double.TryParse(token, out _);
        }

        private static bool HasHigherPriority(string op1, string op2)
        {
            return OperatorPriority[op1] > OperatorPriority[op2];
        }

        private static double ExecuteOperation(string op, double a, double b)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    if (b == 0) throw new DivideByZeroException("Division by zero.");
                    return a / b;
                case "^":
                    return Math.Pow(a, b);
                case "mod":
                    return a % b;
                case "min":
                    return Math.Min(a, b);
                case "max":
                    return Math.Max(a, b);
                default:
                    throw new InvalidOperationException($"Unsupported operator: {op}");
            }
        }
        private static double ExecuteUnaryOperation(string op, double a)
        {
            switch (op)
            {
                case "sqrt":
                    if (a < 0) throw new InvalidOperationException("Cannot compute square root of a negative number.");
                    return Math.Sqrt(a);
                case "ln":
                    if (a <= 0) throw new InvalidOperationException("Logarithm undefined for zero or negative values.");
                    return Math.Log(a);
                case "log":
                    if (a <= 0) throw new InvalidOperationException("Logarithm undefined for zero or negative values.");
                    return Math.Log10(a);
                case "sin":
                    return Math.Sin(a);
                case "cos":
                    return Math.Cos(a);
                case "tan":
                    return Math.Tan(a);
                case "abs":
                    return Math.Abs(a);
                default:
                    throw new InvalidOperationException($"Unsupported unary operator: {op}");
            }
        }
        public static string ConvertToPostfix(string expression)
        {
            var output = new StringBuilder();
            var operators = new MyStack<string>();


            var tokens = regularExp.Matches(expression).Cast<Match>().Select(m => m.Value).ToArray();

            foreach (var token in tokens)
            {
                if (IsNumber(token))
                {
                    output.Append(token + " ");
                }
                else if (OperatorPriority.ContainsKey(token))
                {
                    while (!operators.IsEmpty() && HasHigherPriority(operators.Peek(), token))
                    {
                        output.Append(operators.Pop() + " ");
                    }
                    operators.Push(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")") 
                {
                    while (operators.Peek() != "(")
                    {
                        output.Append(operators.Pop() + " ");
                    }
                    operators.Pop();
                }
            }
            while (!operators.IsEmpty())
            {
                output.Append(operators.Pop() + " ");
            }

            return output.ToString().Trim();
        }
        public static double CalculatePostfix(string expression)
        {
            var stack = new MyStack<double>();
            var tokens = expression.Split(' ');

            foreach (var token in tokens)
            {
                if (IsNumber(token))
                {
                    stack.Push(double.Parse(token));
                }
                else if (OperatorPriority.ContainsKey(token)) 
                {
                    if (IsUnaryOperator(token)) 
                    {
                        var a = stack.Pop();
                        stack.Push(ExecuteUnaryOperation(token, a));
                    }
                    else
                    {
                        var b = stack.Pop();
                        var a = stack.Pop();
                        stack.Push(ExecuteOperation(token, a, b));
                    }
                }

            }
            return stack.Pop();
        }
    }
}
