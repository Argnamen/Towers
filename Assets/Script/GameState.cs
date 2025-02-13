using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class GameState
{
    public int MovesLeft { get; private set; }
    public bool IsLevelComplete { get; private set; }

    private Level _currentLevel;
    private List<Peg> _pegs;
    private List<Ring> _rings;

    private readonly LevelManager _levelManager;
    private readonly ResultService _resultService;

    public GameState(LevelManager levelManager, List<Peg> pegs, List<Ring> rings, ResultService resultService)
    {
        _levelManager = levelManager;
        _resultService = resultService;

        _currentLevel = _levelManager.CurrentLevel;
        _pegs = pegs;
        _rings = rings;
        MovesLeft = _currentLevel.MaxMoves;
        IsLevelComplete = false;

        // �������������� ������ � ������
        InitializePegs();
    }

    /// <summary>
    /// �������������� ������ � ������ � ������������ � ������� �������.
    /// </summary>
    public void InitializePegs()
    {
        // ������� ��� ������
        foreach (var peg in _pegs)
        {
            peg.Rings.Clear();
        }

        // ��������� ������ �� ������ ������
        for (int i = 0; i < _rings.Count; i++)
        {
            _pegs[0].AddRing(_rings[i]);

            _rings[i].Initialize(i + 1);
        }
    }

    /// <summary>
    /// ������� ����������� ������ �� ������� ������.
    /// </summary>
    /// <param name="ring">������, ������� ����� �����������.</param>
    /// <param name="targetPeg">������� ������.</param>
    /// <returns>True, ���� ��� �������� � ��������, ����� False.</returns>
    public bool TryMoveRing(Ring ring, Peg targetPeg)
    {
        // ��������, ����� �� ����������� ������ �� ������� ������
        if (CanMoveRing(ring, targetPeg))
        {
            // ������� ������ � ������� ������
            ring.CurrentPeg.RemoveRing(ring);

            // ��������� ������ �� ������� ������
            targetPeg.AddRing(ring);

            // ��������� ���������� ���������� �����
            MovesLeft--;

            // ���������, �������� �� �������
            if (CheckWinCondition())
            {
                IsLevelComplete = true;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// ��������, ����� �� ����������� ������ �� ������� ������.
    /// </summary>
    /// <param name="ring">������, ������� ����� �����������.</param>
    /// <param name="targetPeg">������� ������.</param>
    /// <returns>True, ���� ����������� ���������, ����� False.</returns>
    private bool CanMoveRing(Ring ring, Peg targetPeg)
    {
        // ������ ����� �����������, ���� ������� ������ ����� ��� ������� ������ �� ��� ������ ��������

        return targetPeg.Rings.Count == 0 || ring.Size < targetPeg.Rings.Last().Size;
    }

    /// <summary>
    /// ��������, ���������� �� ������� ���������.
    /// </summary>
    /// <returns>True, ���� ������� ��������� ��������� � �������, ����� False.</returns>
    private bool CheckWinCondition()
    {
        // �������� ������� ��������� ����
        string currentState = GetCurrentState();

        // ���������� � ������� ����������
        return currentState == _currentLevel.TargetState.Replace("0", "");
    }

    /// <summary>
    /// ������������ ������ ����� ������ � ���������� ������ ������.
    /// </summary>
    public void UpdateLevel()
    {
        _resultService.SaveResult(_levelManager.CurrentLevel.Number, MovesLeft);

        _levelManager.NextLevel();

        _currentLevel = _levelManager.CurrentLevel;

        _currentLevel = _levelManager.CurrentLevel;
        MovesLeft = _currentLevel.MaxMoves;
        IsLevelComplete = false;
    }

    /// <summary>
    /// �������� ������� ��������� ���� � ���� ������.
    /// </summary>
    /// <returns>������, �������������� ������� ���������.</returns>
    private string GetCurrentState()
    {
        // ��������� ���� �������������� ��� ������, ��� ������ ������ ����������� ��������� ����� �� ���
        // ��������, "3,2,1|0|0" ��������:
        // - �� ������ ������ ������ �������� 3, 2, 1
        // - ������ � ������ ������ �����
        return string.Join("|", _pegs.Select(peg => string.Join(",", peg.Rings.Select(ring => ring.Size))));
    }
}
