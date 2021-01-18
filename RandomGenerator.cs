using System;

namespace MazeRatsNPCGenerator
{
    public class RandomGenerator
    {
        private readonly Random _random = new Random();

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
