namespace TicTacToe.StaticData.Gameplay
{
    public interface IGameplaySettings
    {
        int FieldSize { get; }
        float TurnDuration { get; }
    }
}