using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitFactory : IUnitFactory
{
    private DiContainer _container;
    public IUnit Create(string unitType, IWeapon weapon, Resistances armor)
    {
        Debug.Log("TryCreate");
        var unit = _container.InstantiatePrefabResourceForComponent<IUnit>("Prefabs/Units/"+unitType);
        return unit;
    }

    public UnitFactory(DiContainer container)//unit type
    {
        _container = container;
    }
}
