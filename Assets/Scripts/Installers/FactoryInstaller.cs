using Zenject;

public class FactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RoomFactory>().To<RoomFactory>().AsSingle().NonLazy();
        Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle().NonLazy();
        Container.Bind<AbnormalityFactory>().To<AbnormalityFactory>().AsSingle().NonLazy();
        Container.Bind<ItemFactory>().To<ItemFactory>().AsSingle().NonLazy();
        Container.Bind<WeaponFactory>().To<WeaponFactory>().AsSingle().NonLazy();
    }
}