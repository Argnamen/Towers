using System.Collections.Generic;
using System.Linq;

public class GameState
{
    public int MovesLeft { get; private set; }
    public bool IsLevelComplete { get; private set; }

    private Level _currentLevel;
    private List<Peg> _pegs;

    public GameState(Level level, List<Peg> pegs)
    {
        _currentLevel = level;
        _pegs = pegs;
        MovesLeft = level.MaxMoves;
        IsLevelComplete = false;

        // Инициализируем основы и кольца
        InitializePegs();
    }

    /// <summary>
    /// Инициализирует основы и кольца в соответствии с текущим уровнем.
    /// </summary>
    private void InitializePegs()
    {
        // Очищаем все основы
        foreach (var peg in _pegs)
        {
            peg.Rings.Clear();
        }

        // Пример начального состояния: все кольца на первой основе
        var rings = new List<Ring>
        {
            new Ring(3),
            new Ring(2),
            new Ring(1)
        };

        // Размещаем кольца на первой основе
        foreach (var ring in rings)
        {
            _pegs[0].AddRing(ring);
        }
    }

    /// <summary>
    /// Попытка переместить кольцо на целевую основу.
    /// </summary>
    /// <param name="ring">Кольцо, которое нужно переместить.</param>
    /// <param name="targetPeg">Целевая основа.</param>
    /// <returns>True, если ход допустим и выполнен, иначе False.</returns>
    public bool TryMoveRing(Ring ring, Peg targetPeg)
    {
        // Проверка, можно ли переместить кольцо на целевую основу
        if (CanMoveRing(ring, targetPeg))
        {
            // Убираем кольцо с текущей основы
            ring.CurrentPeg.RemoveRing(ring);

            // Добавляем кольцо на целевую основу
            targetPeg.AddRing(ring);

            // Уменьшаем количество оставшихся ходов
            MovesLeft--;

            // Проверяем, завершен ли уровень
            if (CheckWinCondition())
            {
                IsLevelComplete = true;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Проверка, можно ли переместить кольцо на целевую основу.
    /// </summary>
    /// <param name="ring">Кольцо, которое нужно переместить.</param>
    /// <param name="targetPeg">Целевая основа.</param>
    /// <returns>True, если перемещение допустимо, иначе False.</returns>
    private bool CanMoveRing(Ring ring, Peg targetPeg)
    {
        // Кольцо можно переместить, если целевая основа пуста или верхнее кольцо на ней больше текущего
        return targetPeg.Rings.Count == 0 || ring.Size < targetPeg.Rings.Last().Size;
    }

    /// <summary>
    /// Проверка, достигнуто ли целевое состояние.
    /// </summary>
    /// <returns>True, если текущее состояние совпадает с целевым, иначе False.</returns>
    private bool CheckWinCondition()
    {
        // Получаем текущее состояние игры
        string currentState = GetCurrentState();

        // Сравниваем с целевым состоянием
        return currentState == _currentLevel.TargetState;
    }

    /// <summary>
    /// Получает текущее состояние игры в виде строки.
    /// </summary>
    /// <returns>Строка, представляющая текущее состояние.</returns>
    private string GetCurrentState()
    {
        // Состояние игры представляется как строка, где каждая основа описывается размерами колец на ней
        // Например, "3,2,1|0|0" означает:
        // - На первой основе кольца размеров 3, 2, 1
        // - Вторая и третья основы пусты
        return string.Join("|", _pegs.Select(peg => string.Join(",", peg.Rings.Select(ring => ring.Size))));
    }
}
