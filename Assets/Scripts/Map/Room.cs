using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

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
    [Inject]
    private RoomController _roomController;
    //При створенні отримувати значення і створювати на основі колайдери для дверей
    public delegate void OnClickDelegate(Vector3 clickPosition, Room clickedRoom);
    public event OnClickDelegate OnClick;

    
    void Start()
    {
        doors = GetComponentsInChildren<Door>();
    }

    // Update is called once per frame
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

        OnClick?.Invoke(localCoordinates, this);
    }
    

}
