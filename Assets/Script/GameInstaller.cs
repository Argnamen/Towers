using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<Peg> pegs; // Список основ, который мы задаем в Inspector

    public override void InstallBindings()
    {
        // Привязываем GameState
        Container.Bind<GameState>().AsSingle();

        // Привязываем GameUI (он уже находится на сцене)
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();

        // Привязываем GameController
        Container.Bind<GameController>().AsSingle().NonLazy();

        // Привязываем список основ (Peg), который задан в Inspector
        Container.Bind<List<Peg>>().FromInstance(pegs).AsSingle();
    }
}
