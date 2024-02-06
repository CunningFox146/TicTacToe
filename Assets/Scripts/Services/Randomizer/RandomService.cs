using System;
using TicTacToe.Services.Log;

namespace TicTacToe.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        private readonly DateTime _startDateTime = new DateTime(2020, 1, 1);
        private readonly Random _random;
        
        public RandomService()
        {
            _random = new Random(GetSeed());
        }

        private int GetSeed()
        {
            var timeSpan = DateTime.UtcNow - _startDateTime;
            return (int)timeSpan.TotalSeconds;
        }

        public int GetInRange(int min, int max) => _random.Next(min, max);
        public bool GetBool() => GetInRange(0, 100) <= 50;
    }
}