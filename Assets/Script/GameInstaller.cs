using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<Peg> pegs; // ������ �����
    [SerializeField] private List<Ring> rings; // ������ �����
    [SerializeField] private List<Level> levels = new List<Level>() { new Level(1, 10, "3,2,1|0|0"), new Level(2, 8, "0|3,2,1|0") , new Level(3, 5, "0|0|3,2,1") };

    public override void InstallBindings()
    {
        // ����������� GameState
        Container.Bind<GameState>().AsSingle();

        // ����������� GameUI (�� ��� ��������� �� �����)
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();

        // ����������� GameController
        Container.Bind<GameController>().AsSingle().NonLazy();

        // ����������� ������ ����� (Peg)
        Container.Bind<List<Peg>>().FromInstance(pegs).AsSingle();

        // ����������� ������ ����� (Ring)
        Container.Bind<List<Ring>>().FromInstance(rings).AsSingle();

        // ����������� ������
        Container.Bind<List<Level>>().FromInstance(levels).AsSingle();

        // ����������� ������� ������� (��������, ������)
        Container.Bind<Level>().FromInstance(levels[0]).AsSingle();

        Container.Bind<LevelManager>().AsSingle();

        Container.Bind<ResultService>().AsSingle();
    }
}
