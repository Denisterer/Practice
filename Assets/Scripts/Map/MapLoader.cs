using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.IO;

public class MapLoader : MonoBehaviour
{
    private string filePath = Application.dataPath + "/Config";
    [SerializeField]
    private Vector2 leftTopCellPosition = Vector2.zero;
    [SerializeField]
    private float roomShiftX = 20f;
    [SerializeField]
    private float roomShiftY = 13f;
    private string[] data;
    private string fileName = "RoomsConfig.txt";
    [Inject]
    private RoomController _roomController;

    void Start()
    {
        string[] roomString, properties, connections;
        data = File.ReadAllLines(filePath + "/" + fileName);
        foreach (string line in data)
        {
            roomString = line.Split(" ");
            foreach (string room in roomString)
            {
                properties = room.Split("|");
                connections = properties[1].Split("-");
            }
        }
        Debug.Log(data[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
