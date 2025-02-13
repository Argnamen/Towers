using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : IInitializable
{
    private readonly GameState _gameState;
    private readonly GameUI _gameUI;
    private readonly List<Peg> _pegs;

    private Ring _lastRing;

    public GameController(GameState gameState, GameUI gameUI, List<Peg> pegs)
    {
        _gameState = gameState;
        _gameUI = gameUI;
        _pegs = pegs;
    }

    public void Initialize()
    {
        // ������������� UI
        _gameUI.UpdateMovesLeft(_gameState.MovesLeft);
    }

    public void HandleRingSelection(Ring ring)
    {
        // �������� ������� ������ (��������, ����� Raycast)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Peg targetPeg = hit.collider.GetComponent<Peg>();
            if (targetPeg != null)
            {
                HandleRingMove(ring, targetPeg);
            }
            else
            {
                _lastRing = ring;
            }
        }
    }

    public void HandlePegSelection(Peg peg)
    {
        // �������� ������� ������ (��������, ����� Raycast)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (_lastRing != null)
            {
                HandleRingMove(_lastRing, peg);

                _lastRing = null;
            }
        }
    }

    public void HandleRingMove(Ring ring, Peg targetPeg)
    {
        if (_gameState.TryMoveRing(ring, targetPeg))
        {
            // ���������� UI
            _gameUI.UpdateMovesLeft(_gameState.MovesLeft);

            if (_gameState.IsLevelComplete)
            {
                _gameUI.ShowLevelComplete();

            }
        }
        else
        {
            // �������� ��������� � ������������ ����
            _gameUI.ShowInvalidMoveMessage();
        }
    }

    public void NextLevel()
    {
        _gameState.UpdateLevel();
        _gameState.InitializePegs();
        _gameUI.UpdateMovesLeft(_gameState.MovesLeft);
    }

    public void RestartLevel()
    {
        // ������������������ ������ � ������
        _gameState.InitializePegs();

        // ��������� UI
        _gameUI.UpdateMovesLeft(_gameState.MovesLeft);
    }
}