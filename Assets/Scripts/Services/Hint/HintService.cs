using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Randomizer;
using TicTacToe.Services.Rules;
using TicTacToe.Util;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public class HintService : IHintService
    {
        private const int FreeTilesForMiniMax = 9;
        
        private readonly MiniMax _miniMax;
        private readonly IRandomService _randomService;

        public HintService(IGameRules gameRules, IRandomService randomService)
        {
            _miniMax = new MiniMax(gameRules);
            _randomService = randomService;
        }

        public async UniTask<Vector2Int?> GetBestMove(GameTile[,] board, IPlayer player, IPlayer other)
        {
            var task = Task<Vector2Int?>.Factory.StartNew(() => GetBestMoveSync(board, player, other));
            return await task.AsUniTask();
        }

        public Vector2Int? GetBestMoveSync(GameTile[,] sourceBoard, IPlayer player, IPlayer other)
        {
            var freeTiles = sourceBoard.GetFreeTilesCount();

            switch (freeTiles)
            {
                case 0:
                    return null;
                case > FreeTilesForMiniMax:
                    return _randomService.GetRandomTile(sourceBoard);
                default:
                {
                    var board = sourceBoard.CloneBoard();
                    return _miniMax.GetBestMove(board, player, other);
                }
            }
        }
    }
}