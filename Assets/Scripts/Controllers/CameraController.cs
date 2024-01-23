using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
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
            if (currentPosition.leftRoom)
            {
                currentPosition = currentPosition.leftRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isUsingCamera)
        {
            if (currentPosition.rightRoom)
            {
                currentPosition = currentPosition.rightRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isUsingCamera)
        {
            if (currentPosition.upperRoom)
            {
                currentPosition = currentPosition.upperRoom;
                _camera.transform.position = new Vector3(currentPosition.transform.position.x, currentPosition.transform.position.y, _camera.transform.position.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isUsingCamera)
        {
            if (currentPosition.lowerRoom)
            {
                currentPosition = currentPosition.lowerRoom;
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

