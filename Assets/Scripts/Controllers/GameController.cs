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
        foreach (Room room in _roomController.rooms)
        {
            room.OnClick += OnRoomClick;
            foreach(Door door in room.doors)
            {
                door.OnDoorEnter += TransferEmployee;
            }
        }
        foreach(Employee employee in FindObjectsOfType<Employee>())
        {
            employee.OnClick += OnPersonClick;
        }
    }
    void TransferEmployee(Door door, IUnit unit)
    {
        (Room nextRoom, Vector3 position) = _roomController.SearchForDestanationPoint(door);

        if (unit != null)
        {
            if (unit.GetCurrentState() == States.Move)
            {

                unit.MoveToRoom(nextRoom, position);
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
                Debug.Log(room.transform.position);
            }
            Debug.Log("EndOfPath");
            controledPerson.SetMove();
            controledPerson.transform.SetParent(clickedRoom.transform);
            controledPerson.transform.localPosition = new Vector3(clickPosition.x, clickPosition.y,clickPosition.z);
            //Debug.Log(controledPerson.transform.localPosition.ToString());
        }
    }
    void OnPersonClick(Employee person, StateController controller)
    {
        if(controledPerson!=null)
        {
            controledPerson.isControlled = false;
            
        }
        controledPerson = person;
        controller.ChangeCurrentState(States.Move);
    }

}
