using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Dictionary<Room, Door> entrances;
    public Room nextRoom;
    public Door currentDoor;
    public float value = 1f;
    public bool isChecked = false;

    public Connection(Door door, Room conectedRoom)
    {
        currentDoor = door;
        nextRoom = conectedRoom;
    }
    public void GetConnection(Room room)
    {
    }
}
