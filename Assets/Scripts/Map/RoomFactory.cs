using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class RoomFactory
{
    private DiContainer _container;
    private GameObject[] prefabs;
    private Dictionary<int, string> _roomPrefabs;

    public RoomFactory(DiContainer container)
    {
        _container = container;
        prefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms");
        _roomPrefabs = new Dictionary<int, string>
        {
            {1,  "BaseRoom"},
            {2,  "Coridore"},
            {3,  "ContaimentRoom"},
        };
    }

    // Update is called once per frame
    public Room Create(int index)
    {
        int prefabIndex=0;
        if (_roomPrefabs.ContainsKey(index)){
            prefabIndex = GetPrefabIndexByName(_roomPrefabs[index]);
            var room = _container.InstantiatePrefabForComponent<Room>(prefabs[prefabIndex]);
            return room;
        }
        else
        {
            return null;
        }
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
