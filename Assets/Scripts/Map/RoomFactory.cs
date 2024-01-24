using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class RoomFactory
{
    private DiContainer _container;
    private GameObject[] prefabs;
    public RoomFactory(DiContainer container)
    {
        this._container = container;
        prefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms");

    }

    // Update is called once per frame
    public Room Create(int index)
    {
        int prefabIndex=0;
        switch (index)
        {
            case -1:
                {
                    return null;
                }
            case 1:
                {
                    prefabIndex = GetPrefabIndexByName("BaseRoom");
                }break;
            case 2:
                {
                    prefabIndex = GetPrefabIndexByName("Coridore");
                }break;
            case 3:
                {
                    prefabIndex = GetPrefabIndexByName("ContaimentRoom");
                }break;
            default:
                {
                    return null;
                }
        }
        if (prefabIndex ==-1)
        {
            return null;
        }
        var room = _container.InstantiatePrefabForComponent<Room>(prefabs[prefabIndex]);
        return room;
    }
    private int GetPrefabIndexByName(string name)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == name)
                return i;
        }
        return -1; 
    }
    
}
