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

        // ������ ���������� ���������: ��� ������ �� ������ ������
        var rings = new List<Ring>
        {
            new Ring(3),
            new Ring(2),
            new Ring(1)
        };

        // ��������� ������ �� ������ ������
        foreach (var ring in rings)
        {
            _pegs[0].AddRing(ring);
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
        return currentState == _currentLevel.TargetState;
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
