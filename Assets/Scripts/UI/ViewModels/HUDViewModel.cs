using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class HUDViewModel : IBindingContext
    {
        public IProperty<bool> IsHintVisible { get; }
        public IProperty<Vector3> HintPosition { get; }
        public ICommand HintCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand ExitCommand { get; }
        
        public HUDViewModel()
        {
            IsHintVisible = new Property<bool>(false);
            HintPosition = new Property<Vector3>();
            
            HintCommand = new Command(ShowHint);
            UndoCommand = new Command(Undo);
            ExitCommand = new Command(Exit);
        }

        private void ShowHint()
        {
            throw new System.NotImplementedException();
        }

        private void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}