using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public List<Connection> connections = new List<Connection>();//кімнати не повинні самі себе додавати в контроллер
    public List<Door> doors;
    void Start()
    {
        //foreach (Room room in FindObjectsOfType<Room>())
        //{
        //    rooms.Add(room);
        //    //room.OnClick += OnRoomClick;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
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
                        connections.Add(connectionTMP);

                        leftDoor.connectedRoom = connectionTMP;
                    }
                    if (right)
                    {
                        connectionTMP = new Connection(rightDoor, roomMatrix[i, j + 1]);
                        roomMatrix[i, j].rightRoom = connectionTMP;
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
                        connections.Add(connectionTMP);
                    }

                }
            }
        }
    }
    public void AddRoom(Room room)
    {
        rooms.Add(room);
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
    
}
