using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.Log;
using TicTacToe.Services.Skin;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;

namespace TicTacToe.UI.ViewModels
{
    public class SkinPopupViewModel : IBindingContext
    {
        private readonly ILogService _log;
        private readonly ISkinService _skinService;
        private readonly IViewStackService _viewStack;

        public ICommand ApplyCommand { get; }
        public IProperty<string> SkinName { get; }
        
        public SkinPopupViewModel(ISkinService skinService, IViewStackService viewStack, ILogService log)
        {
            _skinService = skinService;
            _viewStack = viewStack;
            _log = log;

            SkinName = new Property<string>(_skinService.CurrentSkin);
            ApplyCommand = new AsyncCommand(ApplySkin);
        }

        private async UniTask ApplySkin(CancellationToken cancellationToken)
        {
            var skinName = SkinName.Value.ToLower();
            try
            {
                await _skinService.SetSkin(skinName);
            }
            catch (Exception exception)
            {
                _log.LogError($"Failed to load skin {skinName}: {exception.Message}");
                SkinName.Value = SkinItemNames.DefaultSkin;
            }
            finally
            {
                _viewStack.PopView();
            }
        }
    }
}