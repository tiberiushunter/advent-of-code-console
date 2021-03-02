using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
    class Day18 : DayBase
    {
        private readonly string[] _input;

        /// <summary>
        /// --- Day 18: Operation Order ---
        /// </summary>
        public Day18()
        {
            _input = Program.GetInput(2020, 18).Split("\n");
        }

        /// <summary>
        /// --- Day 18: Operation Order; Part A ---
        /// </summary>
        private protected override string PartA()
        {
            double finalSum = 0d;
            for (int i = 0; i < _input.Length; i++)
            {
                // Remove all the whitespace
                string line = Regex.Replace(_input[i], @"\s+", "");

                Stack<double> values = new Stack<double>();
                Stack<char> operands = new Stack<char>();

                operands.Push(Token.LeftBracket);

                for (int j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case Token.Addition:
                            (operands, values) = Calculate(operands, values);
                            operands.Push(Token.Addition);
                            break;
                        case Token.Multiply:
                            (operands, values) = Calculate(operands, values);
                            operands.Push(Token.Multiply);
                            break;
                        case Token.LeftBracket:
                            operands.Push(Token.LeftBracket);
                            break;
                        case Token.RightBracket:
                            (operands, values) = Calculate(operands, values);
                            operands.Pop();
                            break;
                        default:
                            values.Push(char.GetNumericValue(line[j]));
                            break;
                    }
                }
                (_, values) = Calculate(operands, values);
                finalSum += values.Single();
            }

            return finalSum.ToString();
        }

        /// <summary>
        /// --- Day 18: Operation Order; Part B ---
        /// </summary>
        private protected override string PartB()
        {
            double finalSum = 0d;
            for (int i = 0; i < _input.Length; i++)
            {
                // Remove all the whitespace
                string line = Regex.Replace(_input[i], @"\s+", "");

                Stack<double> values = new Stack<double>();
                Stack<char> operands = new Stack<char>();

                operands.Push(Token.LeftBracket);

                for (int j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case Token.Addition:
                            (operands, values) = Calculate(operands, values, Token.Addition);
                            operands.Push(Token.Addition);
                            break;
                        case Token.Multiply:
                            (operands, values) = Calculate(operands, values);
                            operands.Push(Token.Multiply);
                            break;
                        case Token.LeftBracket:
                            operands.Push(Token.LeftBracket);
                            break;
                        case Token.RightBracket:
                            (operands, values) = Calculate(operands, values);
                            operands.Pop();
                            break;
                        default:
                            values.Push(char.GetNumericValue(line[j]));
                            break;
                    }
                }
                (_, values) = Calculate(operands, values);
                finalSum += values.Single();
            }

            return finalSum.ToString();
        }

        /// <summary>
        ///  Performs the calculation of the current stacks
        /// </summary>
        /// <param name="operands">Current stack of operators.</param>
        /// <param name="values">Current stack of values.</param>
        /// <param name="precedence">Optional; Sets the precedence value which is used in Part 2.</param>
        /// <returns>Both operands and values stacks.</returns>
        private static (Stack<char> operands, Stack<double> values) Calculate(Stack<char> operands, Stack<double> values, char? precedence = Token.Multiply)
        {
            while (operands.Peek() != Token.LeftBracket && (precedence == Token.Multiply || operands.Peek() != Token.Multiply))
            {
                switch (operands.Pop())
                {
                    case Token.Addition:
                        values.Push(values.Pop() + values.Pop());
                        break;
                    case Token.Multiply:
                        values.Push(values.Pop() * values.Pop());
                        break;
                }
            }

            return (operands, values);
        }

        /// <summary>
        /// Used to store the various tokens
        /// </summary>
        internal sealed class Token
        {
            public const char Addition = '+';
            public const char Multiply = '*';
            public const char LeftBracket = '(';
            public const char RightBracket = ')';
        }
    }
}