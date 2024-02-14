using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponFactory
{
    private DiContainer _container;
    public IWeapon Create(string weaponName, string bulletType)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Bullets/" + bulletType);

        Debug.Log("TryCreate");
        var weapon = _container.InstantiatePrefabResourceForComponent<IWeapon>("Prefabs/Weapons/" + weaponName);
        weapon.bulletPrefab = prefab;
        weapon.weaponParams = Resources.Load<WeaponParams>("ScriptableObjects/Weapons/" + weaponName);
        return weapon;
    }

    public WeaponFactory(DiContainer container)
    {
        _container = container;
    }
}
