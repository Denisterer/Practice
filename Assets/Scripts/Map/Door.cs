using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Door : MonoBehaviour
{
    public ConectionType conectionType;
    
    public Room currentRoom;
    public Connection connectedRoom;
    public Door connectedDoor;
    [Inject]
    private RoomController controller;
    void Start()
    {
        currentRoom = GetComponentInParent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Door[] doors = connectedRoom.nextRoom.GetComponentsInChildren<Door>();
        foreach(Door door  in doors)
        {
            if (door.connectedRoom.nextRoom == currentRoom)
            {
                connectedDoor = door;
                break;
            }
        }
        Employee unit = otherCollider.GetComponent<Employee>();

        if (unit!=null)
        {
            if (unit.controller.currentState.type == States.Move)
            {
                unit.MoveToRoom(connectedRoom.nextRoom);
                otherCollider.transform.SetParent(connectedRoom.nextRoom.transform);//івент на входження в кімнату
                unit.controller.ChangeCurrentState(States.Idle);
                otherCollider.transform.localPosition = connectedDoor.transform.localPosition;//Логіку дверей в більшості перенести в кімнату.

            }
        }
        //unit.stateController.currentState.OnDoorEnter();
    }
}
