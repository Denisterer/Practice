using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoxSpawner : MonoBehaviour
{
    public event Action<Box> OnBoxSpawn;

    [Inject]
    ItemFactory itemFactory;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseUp()
    {
            Box box = itemFactory.Create("Box");

            box.transform.SetParent(transform, false);
            OnBoxSpawn.Invoke(box);

    }
}
