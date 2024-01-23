using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject doorPrefab;
    public override void InstallBindings()
    {
        Container.Bind<Door>().FromComponentInNewPrefab(doorPrefab).AsTransient();//������� factory,  ����� �������
        Container.Bind<StateController>().ToSelf().AsTransient();
    }
}