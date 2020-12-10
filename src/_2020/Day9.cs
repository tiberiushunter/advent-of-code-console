using System;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day9 : DayBase
    {
        private readonly string _input;
        private long[] _inputArr;

        /// <summary>
        /// --- Day 9: Encoding Error ---
        /// </summary>
        public Day9()
        {
            _input = Program.GetInput(2020, 9);
            _inputArr = _input.Split('\n').Select(n => Convert.ToInt64(n)).ToArray();
        }

        /// <summary>
        /// --- Day 9: Encoding Error; Part A ---
        /// </summary>
        private protected override string PartA()
        {
            return FindInvalidNum(25).ToString();
        }

        /// <summary>
        /// --- Day 9: Encoding Error; Part B ---
        /// </summary>
        private protected override string PartB()
        {
            long partA = FindInvalidNum(25);
            long answer = 0L;

            long[] contiguousRange;
            bool sumFound = false;
            int currentPreamble = 2;

            while (!sumFound)
            {
                for (int i = currentPreamble - 1; i < _inputArr.Length; i++)
                {
                    long total = 0;
                    contiguousRange = new long[currentPreamble];

                    for (int k = 0; k < currentPreamble; k++)
                    {
                        total += _inputArr[i - k];
                        contiguousRange[k] = _inputArr[i - k];
                    }

                    if (partA == total)
                    {
                        var min = contiguousRange.OrderBy(p => p).First();
                        var max = contiguousRange.OrderByDescending(p => p).First();

                        answer = min + max;
                        sumFound = true;
                    }
                }
                currentPreamble++;
            }

            return answer.ToString();
        }

        /// <summary>
        /// Finds the invalid number in the list with a preamble
        /// </summary>
        /// <remarks>
        /// Skips the first x number of items in the input based on the preamble value and
        /// then loops through the input to ensure the sum of the previous figures add 
        /// to the current item in the list. When it doesn't it returns the current item
        /// in the list as the invalid number.
        /// </remarks>
        /// <param name="preamble">Preamble value, determins how many items to look back on.</param>
        /// <returns>The invalid number from the list.</returns>
        private long FindInvalidNum(int preamble)
        {
            for (int i = preamble; i <= _inputArr.Length; i++)
            {
                bool sumFound = false;

                for (int j = i - preamble; j < i; j++)
                {
                    for (int k = i - preamble; k < i; k++)
                    {
                        if (_inputArr[i] == _inputArr[j] + _inputArr[k])
                        {
                            sumFound = true;
                        }
                    }
                }

                if (!sumFound)
                {
                    return _inputArr[i];
                }

            }
            return 0;
        }
    }
}