using System.ComponentModel;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameState>().AsSingle();
        Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameController>().AsSingle().NonLazy();
    }
}
