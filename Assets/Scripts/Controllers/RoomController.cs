using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.ParticleSystem;

public class RoomController : MonoBehaviour
{
    [Inject]
    private MapLoader mapLoader;
    public List<Room> rooms = new List<Room>();
    public List<Connection> connections = new List<Connection>();//кімнати не повинні самі себе додавати в контроллер
    public List<Door> doors;
    void Start()
    {
        (Room[,] rooms, Dictionary<Room, string[]> dictionary) = mapLoader.Create();
        this.InitAndEstablishConnections(rooms, dictionary);
        CameraController controller = FindAnyObjectByType<CameraController>();
        controller.Init();
    }
    public Vector3 SearchForDestanationPoint(Door door, Room destination)
    {
        Room currentRoom = door.GetComponentInParent<Room>();
        Vector3 position = Vector3.zero;
        Connection currentConection = GetConnection(door, destination);
        if (currentConection == null) 
        {
            return position;
        }
        foreach (Connection connection in destination.connections)
        {
            if (connection.nextRoom == currentRoom) 
            {
                position = connection.currentDoor.transform.localPosition;
            }
        }
        return position;
    }
    public Connection GetConnection(Door door,Room destination) 
    {
        foreach (Connection connection in connections) 
        {
            if(connection.currentDoor == door && connection.nextRoom == destination)
            {
                return connection;
            }
        }
        return null;
    }


    public void InitAndEstablishConnections(Room[,] roomMatrix, Dictionary<Room, string[]> connection)
    {
        bool left, right, up, down,isLeftStairs;
        Connection connectionTMP;
        Door[] doors;

        for (int i = 0; i < roomMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < roomMatrix.GetLength(1); j++)
            {
                left = false; right = false; up = false; down = false; isLeftStairs = false;
                if (!roomMatrix[i, j])
                {
                    continue;
                }
                rooms.Add(roomMatrix[i, j]);
                if (connection.ContainsKey(roomMatrix[i, j]))
                {
                    foreach( string key in connection[roomMatrix[i, j]])
                    {
                        switch (key)
                        {
                            case "l":
                                {
                                    left = true;
                                }break;
                            case "r":
                                {
                                    right = true;
                                }break;
                            case "u":
                                {
                                    up = true;
                                }break;
                            case "d":
                                {
                                    down = true;
                                }break;
                        }
                    }
                    doors = roomMatrix[i, j].GetComponentsInChildren<Door>();
                    Door leftDoor = GetDoorByName(doors, "LeftDoor");
                    Door rightDoor = GetDoorByName(doors, "RightDoor");
                    if (left)
                    {
                        connectionTMP = new Connection(leftDoor, roomMatrix[i, j - 1]);
                        roomMatrix[i,j].leftRoom = connectionTMP;
                        roomMatrix[i, j].connections.Add(connectionTMP);
                        connections.Add(connectionTMP);

                        leftDoor.connectedRoom = connectionTMP;
                    }
                    if (right)
                    {
                        connectionTMP = new Connection(rightDoor, roomMatrix[i, j + 1]);
                        roomMatrix[i, j].rightRoom = connectionTMP;
                        roomMatrix[i, j].connections.Add(connectionTMP);
                        connections.Add(connectionTMP);
                        isLeftStairs = true;

                        rightDoor.connectedRoom = connectionTMP;

                    }
                    if (up)
                    {
                        if(isLeftStairs)
                        {
                            connectionTMP = new Connection(leftDoor, roomMatrix[i-1, j]);
                            leftDoor.connectedRoom = connectionTMP;
                        }
                        else
                        {
                            connectionTMP = new Connection(rightDoor, roomMatrix[i-1, j]);
                            rightDoor.connectedRoom = connectionTMP;

                        }
                        roomMatrix[i, j].upperRoom = connectionTMP;
                        roomMatrix[i, j].connections.Add(connectionTMP);
                        connections.Add(connectionTMP);
                    }
                    if (down)
                    {
                        if (isLeftStairs)
                        {
                            connectionTMP = new Connection(leftDoor, roomMatrix[i + 1, j]);
                            leftDoor.connectedRoom = connectionTMP;

                        }
                        else
                        {
                            connectionTMP = new Connection(rightDoor, roomMatrix[i + 1, j]);
                            rightDoor.connectedRoom = connectionTMP;

                        }
                        roomMatrix[i, j].lowerRoom = connectionTMP;
                        roomMatrix[i, j].connections.Add(connectionTMP);

                        connections.Add(connectionTMP);
                    }

                }
            }
        }
    }
    public void ResetSettings()
    {
        foreach (Connection connection in connections)
        {
            connection.isChecked = false;
            connection.value = 1f;
        }
        foreach( Room room in rooms)
        {
            room.sum = 0;
            room.isChecked = false;
        }
    }

    private Door GetDoorByName(Door[] doors, string name)
    {
        foreach(Door door in doors)
        {
            if(door.name == name)
            {
                return door;
            }
        }
        return null;
    }
    public Room GetFirstRoom()
    {
        return rooms[0];
    }
    private void SetPathModifier(Room room, float positionInRoom)
    {
        foreach (Connection connection in room.connections)
        {
            if (connection.currentDoor.name == "LeftDoor")
            {
                connection.value += positionInRoom;
            }
            else
            {
                connection.value-= positionInRoom;
            }
        }
    }
    public LinkedList<Room> GetShortestPath(Room startRoom, float startPositionX, Room destinationRoom, float destinationPositionX)
    {
        SetPathModifier(startRoom,startPositionX);
        SetPathModifier(destinationRoom, destinationPositionX);
        LinkedList<Room> resultList = new LinkedList<Room>();
        List<Room> checkedRooms = new List<Room> { startRoom };
        if (startRoom == destinationRoom)
        {
            resultList.AddFirst(startRoom);

            return resultList;
        }
        startRoom.isChecked = true;
        while (checkedRooms.Count<rooms.Count)
        {
            bool check = false;
            float min = float.MaxValue;
            float sum;
            Room linkedRoom = null;
            Room previous = null;

            for (int i=0;i<checkedRooms.Count;i++)
            {
                check = false;
                foreach (var connection in checkedRooms[i].connections)
                {
                    if (!connection.nextRoom.isChecked)
                    {
                        sum = checkedRooms[i].sum;
                        check = true;

                        if (min > sum + connection.value)
                        {
                            min = sum + connection.value;
                            linkedRoom = connection.nextRoom;
                            previous = checkedRooms[i];
                        }
                        
                    }
                }
            }
            if (check == false)
            {
                break;
            }
            linkedRoom.isChecked = true;
            linkedRoom.sum = min;
            checkedRooms.Add(linkedRoom);
            foreach(Connection connection in linkedRoom.connections)
            {
                if (connection.nextRoom == previous)
                {
                    connection.isChecked = true;
                    break;
                }
            }
            if (linkedRoom == destinationRoom)
            {
                Room currentRoom = linkedRoom;
                resultList.AddFirst(currentRoom);
                while (currentRoom != startRoom)
                {
                    
                    foreach (var connection in currentRoom.connections)
                    {
                        if (connection.isChecked)
                        {
                            currentRoom = connection.nextRoom;
                            break;
                        }
                    }
                    resultList.AddFirst(currentRoom);
                }
                break; 
            }
            
        }
        ResetSettings();
        return resultList;
    }
}
