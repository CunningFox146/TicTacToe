using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;
using TicTacToe.Util;

namespace TicTacToe.Services.Rules
{
    public class TicTacToeRules : IGameRules
    {
        public IPlayer GetWinner(GameTile[,] board)
        {
            return CheckHorizontal(board)
                   ?? CheckVertical(board)
                   ?? CheckDiagonal(board);
        }
        
        public bool IsTie(GameTile[,] board)
        {
            if (GetWinner(board) is not null)
                return false;
            
            var boardSize = board.Length;
            var tilesCount = 0;
            foreach (var tile in board)
            {
                if (tile.IsOccupied)
                    tilesCount++;
            }

            return tilesCount == boardSize;
        }

        public int GetBoardScore(GameTile[,] board, IPlayer player)
        {
            var winner = GetWinner(board);
            var playerMoves = 0;
            
            foreach (var tile in board)
            {
                if (tile.Player == player)
                    playerMoves++;
            }

            return winner == player ? 10 + board.GetBoardSize() - playerMoves : -10 - playerMoves;
        }

        private IPlayer CheckHorizontal(GameTile[,] board)
        {
            var boardSize = board.GetBoardSize();
            for (var x = 0; x < boardSize; x++)
            {
                var player = board[x, 0].Player;
                if (player is null)
                    continue;

                for (var y = 0; y < boardSize; y++)
                {
                    var otherPlayer = board[x, y].Player;
                    if (otherPlayer != player)
                        break;

                    if (y == boardSize - 1)
                        return player;
                }
            }

            return null;
        }

        private IPlayer CheckVertical(GameTile[,] board)
        {
            var boardSize = board.GetBoardSize();
            for (var y = 0; y < boardSize; y++)
            {
                var player = board[0, y].Player;
                if (player is null)
                    continue;
        
                for (var x = 0; x < boardSize; x++)
                {
                    var otherPlayer = board[x, y].Player;
                    if (otherPlayer != player)
                        break;
    
                    if (x == boardSize - 1)
                        return player;
                }
            }
            
            return null;
        }

        private IPlayer CheckDiagonal(GameTile[,] board)
        {
            var boardSize = board.GetBoardSize();
            
            var player = board[0, 0].Player;
            if (player is null)
                return null;
            
            for (var i = 1; i < boardSize; i++)
            {
                var tile = board[i, i].Player;
                if (tile != player)
                    break;
    
                if (i == boardSize - 1)
                    return player;
            }
    
            player = board[0, boardSize - 1].Player;
            if (player is null)
                return null;
            for (var i = 1; i < boardSize; i++)
            {
                var tile = board[i, boardSize - i - 1].Player;
                if (tile != player)
                    break;
    
                if (i == boardSize - 1)
                    return player;
            }

            return null;
        }
    }
}