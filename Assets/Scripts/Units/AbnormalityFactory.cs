using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbnormalityFactory : IUnitFactory
{
    private DiContainer _container;
    GameObject[] prefabs;
    public IUnit Create(string name, IWeapon weapon, Resistances armor)
    {
        Debug.Log("TryCreate");
        var unit = _container.InstantiatePrefabResourceForComponent<Abnormality>("Prefabs/Abnormalities/" + name);
        return unit;
    }
    public Abnormality CreateRandom()
    {
        int index = Random.Range(0, prefabs.Length);
        Debug.Log("TryCreateRandom");
        var unit = _container.InstantiatePrefabForComponent<Abnormality>(prefabs[index]);
        return unit;
    }

    public AbnormalityFactory(DiContainer container)//unit type
    {
        _container = container;
        prefabs = Resources.LoadAll<GameObject>("Prefabs/Abnormalities");
    }
}
