using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Dictionary<Room, Door> entrances;
    public Room firstRoom;
    public Room secondRoom;
    public Door firstDoor;
    public Door secondDoor;//зберігати двері і кімнату в яку ведуть

    public Connection()
    {

    }
    public void GetConnection(Room room)
    {
    }
}
