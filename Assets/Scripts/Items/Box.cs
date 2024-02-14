using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Box : MonoBehaviour
{
    public event Action<IUnit> OnUnitSpawn;

    [Inject]
    private AbnormalityFactory abnoFactory;
    [Inject]
    private WeaponFactory weaponFactory;
    void Start()
    {
        StartCoroutine(WaitToRelease());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitToRelease()
    {
        yield return new WaitForSeconds(10f);

        Debug.Log("Заспавнилось");

        IUnit abno = abnoFactory.CreateRandom();
        IWeapon pistol = weaponFactory.Create("Pistol", "HayBullet");
        abno.GetTransform().SetParent(GetComponentInParent<Room>().transform, false);
        abno.AddWeapon(pistol);
        OnUnitSpawn.Invoke(abno);
        Destroy(gameObject);

    }
}
