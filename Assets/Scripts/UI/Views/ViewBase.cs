using TicTacToe.UI.Factories;
using UnityEngine;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UGUI;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class ViewBase<TViewModel> : CanvasView<TViewModel>, IView 
        where TViewModel : class, IBindingContext
    {
        private IValueConverter[] _valueConverters;
        private ViewModelFactory _viewModelFactory;
        private TViewModel _viewModel;
        protected Canvas canvas;

        [Inject]
        private void Constructor(ViewModelFactory viewModelFactory,  IValueConverter[] valueConverters)
        {
            _viewModelFactory = viewModelFactory;
            _valueConverters = valueConverters;
        }

        protected override void OnInit()
        {
            base.OnInit();
            _viewModel = _viewModelFactory.Create<TViewModel>();
            canvas = GetComponent<Canvas>();
        }

        public virtual void Show() 
            => canvas.enabled = true;

        public virtual void Hide() 
            => canvas.enabled = false;

        public virtual void Kill() 
            => Destroy(gameObject);

        protected override TViewModel GetBindingContext()
            => _viewModel;

        protected override IValueConverter[] GetValueConverters()
            => _valueConverters;
    }
}