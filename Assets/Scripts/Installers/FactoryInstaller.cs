using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RoomFactory>().To<RoomFactory>().AsTransient().NonLazy();

    }
}