using System;

namespace TicTacToe.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        private readonly Random _random;
        private readonly DateTime _startDateTime = new(2020, 1, 1);

        public RandomService()
        {
            _random = new Random(GetSeed());
        }

        public int GetInRange(int min, int max)
        {
            return _random.Next(min, max);
        }

        public bool GetBool()
        {
            return GetInRange(0, 100) <= 50;
        }

        public void Shuffle<T>(T[] array)
        {
            var length = array.Length;
            while (length > 1)
            {
                var newIndex = _random.Next(length--);
                (array[length], array[newIndex]) = (array[newIndex], array[length]);
            }
        }

        private int GetSeed()
        {
            var timeSpan = DateTime.UtcNow - _startDateTime;
            return (int)timeSpan.TotalSeconds;
        }
    }
}