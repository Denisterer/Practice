using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Door : MonoBehaviour
{
    public event Action<Door,IUnit> OnDoorEnter;

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
        IUnit unit = otherCollider.GetComponent<IUnit>();
        OnDoorEnter.Invoke(this,unit);
       
        //unit.stateController.currentState.OnDoorEnter();
    }
}
