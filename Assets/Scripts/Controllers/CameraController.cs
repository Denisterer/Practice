using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    [Inject]
    private RoomController rooms;
    [SerializeField]
    private Vector3 officePosition;
    private bool isUsingCamera = false;
    private Room currentPosition;
    void Start()
    {

        currentPosition = rooms.GetFirstRoom();
        _camera.transform.position = officePosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&isUsingCamera)
        {
            if (currentPosition.leftRoom!=null)
            {
                currentPosition = currentPosition.leftRoom.nextRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isUsingCamera)
        {
            if (currentPosition.rightRoom != null)
            {
                currentPosition = currentPosition.rightRoom.nextRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isUsingCamera)
        {
            if (currentPosition.upperRoom != null)
            {
                currentPosition = currentPosition.upperRoom.nextRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isUsingCamera)
        {
            if (currentPosition.lowerRoom != null)
            {
                currentPosition = currentPosition.lowerRoom.nextRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isUsingCamera = true;
            _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _camera.transform.position = officePosition;
            isUsingCamera = false;

        }
    }
}

