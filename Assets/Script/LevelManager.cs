using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : IInitializable
{
    private readonly List<Level> _levels; // Список уровней
    private int _currentLevelIndex = 0;  // Индекс текущего уровня

    public Level CurrentLevel => _levels[_currentLevelIndex]; // Текущий уровень

    public LevelManager(List<Level> levels)
    {
        _levels = levels;
    }

    public void Initialize()
    {
        // Загружаем первый уровень при старте игры
        LoadLevel(_currentLevelIndex);
    }

    /// <summary>
    /// Загружает уровень по индексу.
    /// </summary>
    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= _levels.Count)
        {
            Debug.LogError("Уровень с таким индексом не существует!");
            return;
        }

        _currentLevelIndex = levelIndex;
        Debug.Log($"Загружен уровень {_currentLevelIndex + 1}");
    }

    /// <summary>
    /// Переходит на следующий уровень.
    /// </summary>
    public void NextLevel()
    {
        if (_currentLevelIndex + 1 < _levels.Count)
        {
            _currentLevelIndex++;
            LoadLevel(_currentLevelIndex);
        }
        else
        {
            Debug.Log("Все уровни пройдены!");
        }
    }

    /// <summary>
    /// Перезагружает текущий уровень.
    /// </summary>
    public void RestartLevel()
    {
        LoadLevel(_currentLevelIndex);
    }
}
