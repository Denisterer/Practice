using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Box : MonoBehaviour
{
    public event Action<IUnit> OnUnitSpawn;

    [Inject]
    private AbnormalityFactory factory;
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

        IUnit abno = factory.CreateRandom();
        abno.GetTransform().SetParent(GetComponentInParent<Room>().transform, false);
        OnUnitSpawn.Invoke(abno);
        Destroy(gameObject);

    }
}
