using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawpoint;
    [SerializeField]
    private string unitType="Employee";
    [Inject]
    IUnitFactory unitFactory;
    [Inject]
    WeaponFactory weaponFactory;
    [Inject]
    SpawnerManager spawnerManager;
    public event Action<IUnit> OnUnitSpawn;

    void Start()
    {
        spawnerManager.OnButtonClick += SpawnUnit;
        spawnerManager.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        spawnerManager.Enable();
        
    }
    private void SpawnUnit(string armor, string bullet)
    {
        Room room = GetComponentInParent<Room>();
        if (room != null)
        {
            IUnit unit = unitFactory.Create(unitType, armor);
            IWeapon pistol = weaponFactory.Create("Pistol", bullet);
            unit.GetTransform().SetParent(room.transform, false);
            unit.GetTransform().localPosition = transform.localPosition + spawpoint.localPosition;
            unit.AddWeapon(pistol);
            OnUnitSpawn.Invoke(unit);
            spawnerManager.Disable();
        }
    }
}
