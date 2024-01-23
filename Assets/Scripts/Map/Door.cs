using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public ConectionType conectionType;
    
    public Room currentRoom;
    public Room connectedRoom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Door[] doors = connectedRoom.GetComponentsInChildren<Door>();

        switch (name)
        {
            case "RightDoor":
                {
                    foreach (Door door in doors)
                    {
                        if (door.name == "LeftDoor")
                        {
                            //connectedDoor = door;

                        }
                    }

                }
                break;
            case "LeftDoor":
                {
                    foreach (Door door in doors)
                    {
                        if (door.name == "RightDoor")
                        {
                            //connectedDoor = door;

                        }
                    }

                }
                break;
        }
        Employee unit = otherCollider.GetComponent<Employee>();

        if (unit!=null)
        {
            if (unit.controller.currentState.type == States.Move)
            {
                unit.MoveToRoom(connectedRoom);
                otherCollider.transform.SetParent(connectedRoom.transform);//івент на входження в кімнату
                unit.controller.ChangeCurrentState(States.Idle);
                //otherCollider.transform.localPosition = connectedDoor.transform.localPosition;//Логіку дверей в більшості перенести в кімнату.

            }
        }
        //unit.stateController.currentState.OnDoorEnter();
    }
}
