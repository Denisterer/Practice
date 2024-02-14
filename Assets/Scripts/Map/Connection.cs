public class Connection
{
    public Room nextRoom;
    public Door currentDoor;
    public float value = 1f;
    public bool isChecked = false;

    public Connection(Door door, Room conectedRoom)
    {
        currentDoor = door;
        nextRoom = conectedRoom;
    }
}
