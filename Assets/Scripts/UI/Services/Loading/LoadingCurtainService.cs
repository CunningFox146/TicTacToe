using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Services.Loading
{
    public class LoadingCurtainService : ILoadingCurtainService
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private LoadingView _activeView;

        public LoadingCurtainService(IUserInterfaceFactory userInterfaceFactory)
        {
            _userInterfaceFactory = userInterfaceFactory;
        }

        public async void ShowLoadingCurtain()
        {
            _activeView = await _userInterfaceFactory.CreateLoadingView();
        }

        public void HideLoadingCurtain()
        {
            if (_activeView)
                _activeView.Hide();

            _activeView = null;
        }
    }
}