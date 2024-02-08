using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class MainMenuViewModel : IBindingContext
    {
        public IProperty<int> Count { get; }
        public ICommand StartCommand { get; }
        
        public MainMenuViewModel()
        {
            Count = new Property<int>();
            StartCommand = new Command(StartGame);
        }

        private void StartGame()
        {
            throw new System.NotImplementedException();
        }
    }
}