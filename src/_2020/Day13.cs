using System;
using System.Linq;

namespace AdventOfCode._2020
{
    class Day13 : DayBase
    {
        private readonly string _input;
        private readonly long _earliestTimestamp;
        private string[] busTimetable;
        private int[] _buses;

        /// <summary>
        /// --- Day 13: Shuttle Search ---
        /// </summary>
        public Day13()
        {
            _input = Program.GetInput(2020, 13);
            busTimetable = _input.Split("\n");
            _earliestTimestamp = Int64.Parse(busTimetable[0]);
        }

        /// <summary>
        /// --- Day 13: Shuttle Search; Part A ---
        /// </summary>
        private protected override string PartA()
        {
            busTimetable = _input.Split("\n");

            busTimetable = busTimetable[1].Split(",")
                        .Where(x => x.Take(1)
                        .All(Char.IsDigit))
                        .ToArray();

            _buses = new int[busTimetable.Length];

            for (int i = 0; i < busTimetable.Length; i++)
            {
                _buses[i] = Int32.Parse(busTimetable[i]);
            }

            bool busFound = false;
            int busToTake = -1;
            int minsToWait = 0;
            int currentTimestamp = 0;

            while (!busFound)
            {
                for (int i = 0; i < _buses.Length; i++)
                {
                    if (currentTimestamp % _buses[i] == 0)
                    {
                        if (currentTimestamp > _earliestTimestamp)
                        {
                            busToTake = _buses[i];
                            minsToWait = currentTimestamp - (int)_earliestTimestamp;
                            busFound = true;
                        }
                    }
                }
                currentTimestamp++;
            }

            return (busToTake * minsToWait).ToString();
        }

        /// <summary>
        /// --- Day 13: Shuttle Search; Part B ---
        /// </summary>
        private protected override string PartB()
        {
            busTimetable = _input.Split("\n");

            busTimetable = busTimetable[1].Split(",");

            _buses = new int[busTimetable.Length];

            for (int i = 0; i < busTimetable.Length; i++)
            {
                if (busTimetable[i] != "x")
                {
                    _buses[i] = Int32.Parse(busTimetable[i]);
                }
                else
                {
                    _buses[i] = 1;
                }
            }

            bool busChainFound = false;
            long currentTimestamp = 0L;
            long offset = 1;

            // Used to keep track of the number of chained buses
            int[] chainedBusIDs = new int[_buses.Length];

            while (!busChainFound)
            {
                for (int i = 0; i < _buses.Length; i++)
                {
                    if ((currentTimestamp + i) % _buses[i] == 0)
                    {
                        if (chainedBusIDs[i] != i)
                        {
                            offset *= _buses[i];
                            chainedBusIDs[i] = i;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (chainedBusIDs[chainedBusIDs.Length - 1] > 0)
                {
                    busChainFound = true;
                }
                else
                {
                    currentTimestamp += offset;
                }
            }

            return currentTimestamp.ToString();
        }
    }
}