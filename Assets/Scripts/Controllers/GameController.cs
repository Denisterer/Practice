using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject]
    private RoomController rooms;
    private Employee controledPerson;
    private void Start()
    {
        foreach (Room room in rooms.rooms)
        {
            room.OnClick += OnRoomClick;
        }
        foreach(Employee employee in FindObjectsOfType<Employee>())
        {
            employee.OnClick += OnPersonClick;
        }
    }
    void OnRoomClick(Vector3 clickPosition, Room clickedRoom)
    {
       
        if (controledPerson != null)
        { 
            controledPerson.transform.SetParent(clickedRoom.transform);
            controledPerson.transform.localPosition = new Vector2(clickPosition.x, clickPosition.y);
            Debug.Log(controledPerson.transform.localPosition.ToString());
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
