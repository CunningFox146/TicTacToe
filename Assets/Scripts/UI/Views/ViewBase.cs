using TicTacToe.UI.ViewStack;
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
        private TViewModel _viewModel;
        protected Canvas canvas;

        public RectTransform Transform => (RectTransform)transform;
        public IViewStackSystem ViewStack { get; set; }

        [Inject]
        private void Constructor(TViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void OnInit()
        {
            base.OnInit();
            canvas = GetComponent<Canvas>();
        }

        public virtual void Show() 
            => canvas.enabled = true;

        public virtual void Hide() 
            => canvas.enabled = false;

        public virtual void Destroy()
        {
            if (gameObject)
                Destroy(gameObject);
        }
        
        protected override TViewModel GetBindingContext()
            => _viewModel;

        protected override IValueConverter[] GetValueConverters()
            => _valueConverters;
    }
}