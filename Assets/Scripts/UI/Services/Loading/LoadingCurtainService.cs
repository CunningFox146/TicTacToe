using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Services.Loading
{
    public class LoadingCurtainService : ILoadingCurtainService
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private LoadingCurtainView _activeCurtainView;

        public LoadingCurtainService(IUserInterfaceFactory userInterfaceFactory)
        {
            _userInterfaceFactory = userInterfaceFactory;
        }

        public async void ShowLoadingCurtain()
        {
            _activeCurtainView = await _userInterfaceFactory.CreateLoadingView();
        }

        public void HideLoadingCurtain()
        {
            if (_activeCurtainView)
                _activeCurtainView.Hide();

            _activeCurtainView = null;
        }
    }
}