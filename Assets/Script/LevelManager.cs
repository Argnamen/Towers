using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : IInitializable
{
    private readonly List<Level> _levels; // ������ �������
    private int _currentLevelIndex = 0;  // ������ �������� ������

    public Level CurrentLevel => _levels[_currentLevelIndex]; // ������� �������

    public LevelManager(List<Level> levels)
    {
        _levels = levels;
    }

    public void Initialize()
    {
        // ��������� ������ ������� ��� ������ ����
        LoadLevel(_currentLevelIndex);
    }

    /// <summary>
    /// ��������� ������� �� �������.
    /// </summary>
    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= _levels.Count)
        {
            Debug.LogError("������� � ����� �������� �� ����������!");
            return;
        }

        _currentLevelIndex = levelIndex;
        Debug.Log($"�������� ������� {_currentLevelIndex + 1}");
    }

    /// <summary>
    /// ��������� �� ��������� �������.
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
            Debug.Log("��� ������ ��������!");
        }
    }

    /// <summary>
    /// ������������� ������� �������.
    /// </summary>
    public void RestartLevel()
    {
        LoadLevel(_currentLevelIndex);
    }
}
