using System;
using System.Threading;

namespace TicTacToe.Services.GameBoard
{
    public interface ICountdownSource
    {
        event Action<float, CancellationToken> CountdownStarted;
    }
}