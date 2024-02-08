using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UGUI;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class ViewBase<TViewModel> : CanvasView<TViewModel>, IView 
        where TViewModel : class, IBindingContext
    {
        private IValueConverter[] _valueConverters;
        private TViewModel _viewModel;
        protected Canvas canvas;

        public RectTransform Transform => (RectTransform)transform;

        [Inject]
        private void Constructor(TViewModel injectedViewModel)
        {
            _viewModel = injectedViewModel;
        }

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
        }


        public virtual void Show()
        {
            
        }
        
        public virtual void Hide()
        {
            
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
        
        protected override TViewModel GetBindingContext()
            => _viewModel;

        protected override IValueConverter[] GetValueConverters()
            => _valueConverters;
    }
}