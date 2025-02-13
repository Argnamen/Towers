using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<Peg> pegs; // Список основ
    [SerializeField] private List<Ring> rings; // Список колец
    [SerializeField] private List<Level> levels = new List<Level>() { new Level(1, 10, "3,2,1|0|0"), new Level(2, 8, "0|3,2,1|0") , new Level(3, 5, "0|0|3,2,1") };

    public override void InstallBindings()
    {
        // Привязываем GameState
        Container.Bind<GameState>().AsSingle();

        // Привязываем GameUI (он уже находится на сцене)
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();

        // Привязываем GameController
        Container.Bind<GameController>().AsSingle().NonLazy();

        // Привязываем список основ (Peg)
        Container.Bind<List<Peg>>().FromInstance(pegs).AsSingle();

        // Привязываем список колец (Ring)
        Container.Bind<List<Ring>>().FromInstance(rings).AsSingle();

        // Привязываем уровни
        Container.Bind<List<Level>>().FromInstance(levels).AsSingle();

        // Привязываем текущий уровень (например, первый)
        Container.Bind<Level>().FromInstance(levels[0]).AsSingle();

        Container.Bind<LevelManager>().AsSingle();

        Container.Bind<ResultService>().AsSingle();
    }
}
