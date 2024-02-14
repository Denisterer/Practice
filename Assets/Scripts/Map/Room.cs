using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string name;
    public bool isChecked=false;
    public int index;
    public float sum = 0;
    public Door[] doors;
    public Connection upperRoom = null;
    public Connection lowerRoom = null;
    public Connection leftRoom = null;
    public Connection rightRoom = null;
    public List<Connection> connections = new List<Connection>();
    public delegate void OnClickDelegate(float clickPositionX, Room clickedRoom);
    public event OnClickDelegate OnClick;

    
    void Start()
    {
        doors = GetComponentsInChildren<Door>();
    }

    public Door GetDoorByDestination(Room destination)
    {
        foreach(Connection connection in connections)
        {
            if (connection.nextRoom == destination)
            {
                return connection.currentDoor;
            }
        }
        return null;
    }
    private void OnMouseUp()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localCoordinates = transform.InverseTransformPoint(clickPosition);
        OnClick?.Invoke(localCoordinates.x, this);
    }
}
