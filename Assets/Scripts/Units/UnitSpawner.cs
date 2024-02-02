using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitSpawner : MonoBehaviour
{

    [Inject]
    IUnitFactory unitFactory;
    public event Action<IUnit> OnUnitSpawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        Room room = GetComponentInParent<Room>();
        if(room != null )
        {
            IUnit unit = unitFactory.Create("Employee", new Pistol(), new Resistances());
        
            unit.GetTransform().SetParent(room.transform,false);
            OnUnitSpawn.Invoke(unit);
        }
        
    }
}
