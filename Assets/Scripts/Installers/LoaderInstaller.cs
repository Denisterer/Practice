using UnityEngine;
using Zenject;

public class LoaderInstaller : MonoInstaller
{
    [SerializeField]
    private MapLoader loader;
    public override void InstallBindings()
    {
        Container.Bind<MapLoader>().ToSelf().FromInstance(loader).AsSingle().NonLazy();

    }
}