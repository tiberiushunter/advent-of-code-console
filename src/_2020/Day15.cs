using System;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day15 : DayBase
    {
        private readonly string _input;
        private int[] _startingNumbers;

        /// <summary>
        /// --- Day 15: Rambunctious Recitation ---
        /// </summary>
        public Day15()
        {
            _input = Program.GetInput(2020, 15);
            _startingNumbers = _input.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        }

        /// <summary>
        /// --- Day 15: Rambunctious Recitation; Part A ---
        /// </summary>
        private protected override string PartA()
        {
            return GetSpokenNumberAtTurn(2020).ToString();
        }

        /// <summary>
        /// --- Day 15: Rambunctious Recitation; Part B ---
        /// </summary>
        private protected override string PartB()
        {
            return GetSpokenNumberAtTurn(30000000).ToString();
        }

        private int GetSpokenNumberAtTurn(int numOfTurns)
        {
            int[] spokenNums = new int[numOfTurns];
            int current = 0;

            // First deal with initial numbers spoken
            for (int i = 0; i < _startingNumbers.Length - 1; i++)
            {
                spokenNums[_startingNumbers[i]] = i + 1;
                current = _startingNumbers[i + 1];
            }

            for (int i = _startingNumbers.Length - 1; i < numOfTurns - 1; i++)
            {
                int spokenNum = spokenNums[current];
                spokenNums[current] = i + 1;
                current = spokenNum == 0 ? 0 : i + 1 - spokenNum;
            }

            return current;
        }
    }
}