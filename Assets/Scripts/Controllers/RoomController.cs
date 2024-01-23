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
    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }
    public Room GetFirstRoom()
    {
        return rooms[0];
    }
    public void BFS(Room start, Room destination)
    {
        Queue<Room> stack = new Queue<Room>();
        stack.Enqueue(start);
        start.isChecked = true;
        while (stack.Count != 0)
        {
            Room currentRoom = stack.Dequeue();
            int Length = 0;

            for (int j = 0; j < currentRoom.doors.Count; j++)
                if (currentRoom.doors[j].connectedRoom.isChecked == false)
                {
                    stack.Enqueue(currentRoom.doors[j].connectedRoom);
                    currentRoom.doors[j].connectedRoom.isChecked = true;
                }
        }
    }
}
