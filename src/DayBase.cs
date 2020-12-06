using System.Drawing;
using Console = Colorful.Console;

namespace AdventOfCode
{
    abstract class DayBase
    {
        public void Solve()
        {
            Console.WriteLineFormatted("Part 1: {0}", Color.Yellow, Color.Gray, PartA());
            Console.WriteLineFormatted("Part 2: {0}\n", Color.Yellow, Color.Gray, PartB());
        }
        private protected abstract string PartA();
        private protected abstract string PartB();
    }
}