using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RoomFactory>().To<RoomFactory>().AsTransient().NonLazy();
        Container.Bind<IUnitFactory>().To<UnitFactory>().AsTransient().NonLazy();
        Container.Bind<AbnormalityFactory>().To<AbnormalityFactory>().AsTransient().NonLazy();
        Container.Bind<ItemFactory>().To<ItemFactory>().AsTransient().NonLazy();


    }
}