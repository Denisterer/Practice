using System.Collections.Generic;
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
            {4,  "EmployeeRoom"},
        };
    }
    public Room Create(int index)
    {
        if (_roomPrefabs.ContainsKey(index)){
            int prefabIndex = GetPrefabIndexByName(_roomPrefabs[index]);
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
