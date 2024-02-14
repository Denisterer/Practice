using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject]
    private RoomController _roomController;
    private Employee controledPerson;
    private void Start()
    {
        UnitSpawner spawner;
        BoxSpawner spawner2;
        foreach (Room room in _roomController.rooms)
        {
            room.OnClick += OnRoomClick;
            foreach(Door door in room.doors)
            {
                door.OnDoorEnter += TransferUnit;
            }
            spawner = room.GetComponentInChildren<UnitSpawner>();
            if (spawner)
            {
                spawner.OnUnitSpawn += RegisterUnit;
            }
             spawner2 = room.GetComponentInChildren<BoxSpawner>();
            if (spawner2)
            {
                spawner2.OnBoxSpawn += RegisterItem;
            }

        }
        foreach(Employee employee in FindObjectsOfType<Employee>())
        {
            employee.OnClick += OnPersonClick;
        }
    }
    public void RegisterItem(Box box)
    {
        box.OnUnitSpawn += RegisterUnit;
    }
    public void RegisterUnit(IUnit unit)
    {
        Employee e =  unit as Employee;
        if (e != null)
        {
            e.OnClick += OnPersonClick;
            e.OnRandomMove += MoveToRandomRoom;
        }
        Abnormality a = unit as Abnormality;
        if (a != null)
        {
            a.OnClick += OnAbnoClick;
            a.OnRandomMove += MoveToRandomRoom;
        }

    }
    public void MoveToRandomRoom(IUnit unit)
    {
        Room destination =_roomController.GetRandomRoom();
        SpriteRenderer renderer = destination.GetComponent<SpriteRenderer>();
        float spriteWidth = renderer.bounds.size.x;
        float position = Random.Range(-spriteWidth / 2 + 4, spriteWidth / 2 - 4);
        SetUnitMove(unit,position, destination);


    }
    void TransferUnit(Door door, IUnit unit)
    {
        Room destinationRoom = unit.GetDestination();
        Room currentRoom = door.GetComponentInParent<Room>();
        Vector3 position = _roomController.SearchForDestanationPoint(door, destinationRoom);
        if (unit != null)
        {
            if (position!=Vector3.zero)
            {
                foreach(IUnit unit2 in currentRoom.GetComponentsInChildren<IUnit>()) 
                {
                    unit2.RemoveTarget(unit);
                }
                foreach (IUnit unit3 in destinationRoom.GetComponentsInChildren<IUnit>())
                {
                    unit3.AddTarget(unit);
                }
                unit.MoveToRoom(position);
            }
        }
    }
    void SetUnitMove(IUnit unit, float positionX, Room destination)
    {
        LinkedList<Room> rooms = _roomController.GetShortestPath(unit.GetCurrentRoom(), unit.GetTransform().localPosition.x, destination, positionX);
        unit.SetMove(rooms, positionX);
    }
    void OnRoomClick(float clickPositionX, Room clickedRoom)
    {
        
        if (controledPerson != null)
        {
            SetUnitMove(controledPerson, clickPositionX, clickedRoom);
        }
    }
    void OnPersonClick(Employee person)
    {
        if(controledPerson!=null)
        {
            controledPerson.isControlled = false;
            
        }
        controledPerson = person;
    }
    void OnAbnoClick(Abnormality abno)
    {
        if (controledPerson != null)
        {

            controledPerson.AddTarget(abno);
            LinkedList<Room> rooms = _roomController.GetShortestPath(controledPerson.GetComponentInParent<Room>(), controledPerson.transform.localPosition.x, abno.GetComponentInParent<Room>(), abno.transform.localPosition.x);

            Debug.Log("GotList");
            if (rooms != null)
            {
                Debug.Log(rooms.Count);
            }
            foreach (Room room in rooms)
            {
                //Debug.Log(room.transform.position);
            }
            Debug.Log("EndOfPath");
            controledPerson.SetMove(rooms, abno.transform.localPosition.x);
        }
    }

}
