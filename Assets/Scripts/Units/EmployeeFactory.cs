using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitFactory : IUnitFactory
{
    private DiContainer _container;
    public IUnit Create(string unitType, IWeapon weapon, Resistances armor)
    {
        var unit = _container.InstantiatePrefabResourceForComponent<Employee>("Prefabs/..."+unitType);
        return unit;
    }

    public UnitFactory(DiContainer container)//unit type
    {
        _container = container;
    }
}
