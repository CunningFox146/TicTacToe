using System.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;
using Zenject;

namespace TicTacToe.UI.Services.Loading
{
    public class LoadingCurtainService : ILoadingCurtainService
    {
        private LoadingCurtainView _activeCurtainView;
        private LoadingCurtainView.Factory _loadingCurtainFactory;

        [Inject]
        private void Constructor(LoadingCurtainView.Factory loadingCurtainFactory)
        {
            _loadingCurtainFactory = loadingCurtainFactory;
        }

        public async void ShowLoadingCurtain() 
            => _activeCurtainView = await CreateLoadingView();

        public void HideLoadingCurtain()
        {
            if (_activeCurtainView)
                _activeCurtainView.Hide();

            _activeCurtainView = null;
        }

        private async Task<LoadingCurtainView> CreateLoadingView() 
            => await _loadingCurtainFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.LoadingCurtainView);
    }
}