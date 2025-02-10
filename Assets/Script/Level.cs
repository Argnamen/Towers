public class Level
{
    public int MaxMoves { get; private set; }
    public string TargetState { get; private set; }

    public Level(int maxMoves, string targetState)
    {
        MaxMoves = maxMoves;
        TargetState = targetState;
    }
}
