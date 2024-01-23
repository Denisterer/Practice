using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [SerializeField]
    private RoomController roomController;
    public override void InstallBindings()
    {
        Container.Bind<RoomController>().ToSelf().FromInstance(roomController).AsSingle().NonLazy();

    }
}