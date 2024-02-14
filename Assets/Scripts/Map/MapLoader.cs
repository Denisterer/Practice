using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.IO;

public class MapLoader : MonoBehaviour
{
    private string filePath = Path.Combine(Application.streamingAssetsPath, "Config");
    [SerializeField]
    private Vector2 leftTopCellPosition = Vector2.zero;
    private Vector2 currentPosition;
    [SerializeField]
    private float roomShiftX = 20f;
    [SerializeField]
    private float roomShiftY = 13f;
    private string[] data;
    private string fileName = "RoomsConfig.txt";
    private Room[,] rooms;
    private Dictionary<Room, string[]> conectionDict = new Dictionary<Room, string[]>();
    [Inject]
    private RoomFactory _roomFactory;


    public (Room[,] rooms, Dictionary<Room, string[]> connectionDicts) Create()
    {
        currentPosition = leftTopCellPosition;
        string[] roomString, properties, connections;
        data = File.ReadAllLines(Path.Combine(filePath, fileName));
        rooms = new Room[data.Length, data[0].Split(" ").Length];
        for (int i = 0; i < data.Length; i++)
        {
            currentPosition.x = leftTopCellPosition.x;
            currentPosition.y -= roomShiftY;
            roomString = data[i].Split(" ");
            for (int j = 0; j < roomString.Length; j++)
            {

                properties = roomString[j].Split("|");
                rooms[i, j] = _roomFactory.Create(int.Parse(properties[0]));
                if (rooms[i, j] == null)
                {
                    currentPosition.x += roomShiftX;
                    continue;
                }
                rooms[i, j].transform.position = currentPosition;
                currentPosition.x += roomShiftX;
                if (properties.Length >= 2)
                {
                    conectionDict.Add(rooms[i, j], properties[1].Split("-"));
                }

            }
        }

        return (rooms, conectionDict);
    }
}
