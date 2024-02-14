using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameMode;
using Zenject;

namespace TicTacToe.Gameplay.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IInstantiator _instantiator;

        public PlayerFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public TController Create<TController>() where TController : IPlayer => 
            _instantiator.Instantiate<TController>();
        
        public IPlayer[] GetPlayers(GameMode mode)
        {
            return mode switch
            {
                GameMode.Bot => new[] { CreatePlayer(), CreateBotPlayer() },
                GameMode.Human => new[] { CreatePlayer(), CreatePlayer() },
                GameMode.TwoBots => new[] { CreateBotPlayer(), CreateBotPlayer() },
                _ => null
            };
        }

        private IPlayer CreateBotPlayer() 
            => Create<BotPlayer>();

        private IPlayer CreatePlayer() 
            => Create<Player>();
    }
}