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
        }
        Abnormality a = unit as Abnormality;
        if (a != null)
        {
            a.OnClick += OnAbnoClick;
        }
        
    }
    void TransferUnit(Door door, IUnit unit)
    {
        Room destinationRoom = unit.GetDestination();
        Room currentRoom = door.GetComponentInParent<Room>();
        Vector3 position = _roomController.SearchForDestanationPoint(door, destinationRoom);
        Debug.Log("Try Transfer");
        if (unit != null)
        {
            Debug.Log("Try Transfer2");

            if (position!=Vector3.zero)
            {
                Debug.Log("Try Transfer3");
                foreach(IUnit unit2 in currentRoom.GetComponentsInChildren<IUnit>()) 
                {
                    unit2.RemoveTarget(unit);
                }
                foreach (IUnit unit3 in destinationRoom.GetComponentsInChildren<IUnit>())
                {
                    unit3.AddTarget(unit);
                }
                unit.MoveToPosition(position);
            }
        }
        //unit.stateController.currentState.OnDoorEnter();
    }
    void OnRoomClick(Vector3 clickPosition, Room clickedRoom)
    {
        
        clickPosition.z = -1;  
        if (controledPerson != null)
        {
            Debug.Log("TryGetList");
            LinkedList<Room> rooms = _roomController.GetShortestPath(controledPerson.GetComponentInParent<Room>(), controledPerson.transform.localPosition.x, clickedRoom, clickPosition.x);
            
            Debug.Log("GotList");
            if(rooms != null)
            {
                Debug.Log(rooms.Count);
            }
            foreach (Room room in rooms)
            {
                //Debug.Log(room.transform.position);
            }
            Debug.Log("EndOfPath");
            controledPerson.SetMove(rooms,clickPosition.x);

            //controledPerson.transform.SetParent(clickedRoom.transform);
            //controledPerson.transform.localPosition = new Vector3(clickPosition.x, clickPosition.y,clickPosition.z);
            //Debug.Log(controledPerson.transform.localPosition.ToString());
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
    void OnAbnoClick(GameObject abno)
    {
        if (controledPerson != null)
        {
            controledPerson.TakeCommand(abno);

        }
    }

}
