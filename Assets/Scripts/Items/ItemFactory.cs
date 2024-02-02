using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemFactory
{
    private DiContainer _container;


    public ItemFactory(DiContainer container)
    {
        _container = container;
    }

    // Update is called once per frame
    public Box Create(string itemName)
    {
        Debug.Log("TryCreate");
        var item = _container.InstantiatePrefabResourceForComponent<Box>("Prefabs/Items/" + itemName);
        return item;
    }
    
}
