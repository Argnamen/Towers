using System.Collections.Generic;

public class GameController : IInitializable
{
    private readonly GameState _gameState;
    private readonly GameUI _gameUI;
    private readonly List<Peg> _pegs;

    public GameController(GameState gameState, GameUI gameUI, List<Peg> pegs)
    {
        _gameState = gameState;
        _gameUI = gameUI;
        _pegs = pegs;
    }

    public void Initialize()
    {
        // Инициализация UI
        _gameUI.UpdateMovesLeft(_gameState.MovesLeft);
    }

    public void HandleRingMove(Ring ring, Peg targetPeg)
    {
        if (_gameState.TryMoveRing(ring, targetPeg))
        {
            // Обновление UI
            _gameUI.UpdateMovesLeft(_gameState.MovesLeft);

            if (_gameState.IsLevelComplete)
            {
                _gameUI.ShowLevelComplete();
            }
        }
        else
        {
            // Показать сообщение о недопустимом ходе
            _gameUI.ShowInvalidMoveMessage();
        }
    }
}
