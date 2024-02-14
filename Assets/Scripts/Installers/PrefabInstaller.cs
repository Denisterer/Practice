using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject canvasPrefab;
    public override void InstallBindings()
    {
        //Container.Bind<StateController>().ToSelf().AsTransient();
        Container.Bind<SpawnerManager>().FromComponentInNewPrefab(canvasPrefab).AsSingle();
    }
}