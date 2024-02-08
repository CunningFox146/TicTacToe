using TicTacToe.UI.ViewModels;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using UnityEngine;
using UnityMvvmToolkit.UGUI;

namespace TicTacToe.UI.Factories
{
    [CreateAssetMenu(menuName = "UI/UserInterfaceAssets")]
    public class UserInterfaceAssets : ScriptableObject
    {
        [field:SerializeField] public ViewStackSystem ViewStackPrefab { get; private set; }
        [field:SerializeField] public MainMenuView MainMenuViewPrefab { get; private set; }
    }
}