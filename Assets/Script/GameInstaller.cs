using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<Peg> pegs; // ������ �����, ������� �� ������ � Inspector

    public override void InstallBindings()
    {
        // ����������� GameState
        Container.Bind<GameState>().AsSingle();

        // ����������� GameUI (�� ��� ��������� �� �����)
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();

        // ����������� GameController
        Container.Bind<GameController>().AsSingle().NonLazy();

        // ����������� ������ ����� (Peg), ������� ����� � Inspector
        Container.Bind<List<Peg>>().FromInstance(pegs).AsSingle();
    }
}
