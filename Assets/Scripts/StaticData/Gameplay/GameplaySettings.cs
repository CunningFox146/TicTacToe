using UnityEngine;

namespace TicTacToe.StaticData.Gameplay
{
    [CreateAssetMenu(menuName = "Static Data/Gameplay Settings")]
    public class GameplaySettings : ScriptableObject, IGameplaySettings
    {
        [field: SerializeField] public int FieldSize { get; private set; }
        [field: SerializeField] public float MoveDuration { get; private set; }
    }
}