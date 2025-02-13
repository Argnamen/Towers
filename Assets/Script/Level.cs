public class Level
{
    public int Number { get; private set; }
    public int MaxMoves { get; private set; }
    public string TargetState { get; private set; }

    public Level(int number ,int maxMoves, string targetState)
    {
        Number = number;
        MaxMoves = maxMoves;
        TargetState = targetState;
    }
}
