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
    public float sum;
    public List<Door> doors = new List<Door>();
    public Connection upperRoom = null;
    public Connection lowerRoom = null;
    public Connection leftRoom = null;
    public Connection rightRoom = null;
    [Inject]
    private RoomController _roomController;
    //При створенні отримувати значення і створювати на основі колайдери для дверей
    public delegate void OnClickDelegate(Vector3 clickPosition, Room clickedRoom);
    public event OnClickDelegate OnClick;

    
    void Start()
    {
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Bounds spriteBounds = spriteRenderer.bounds;
        //BoxCollider2D floor = this.AddComponent<BoxCollider2D>();
        //floor.offset = new Vector2(0, -spriteBounds.size.y/2);
        //floor.size = new Vector2(19.2f, 0.7f);        Debug.Log(floor.size.ToString());

        //this.AddComponent<BoxCollider2D>().isTrigger=true;
        //_roomController.AddRoom(this);
        //if (leftRoom)
        //{
        //    Door door = Instantiate(doorPrefab);
        //    door.name = "LeftDoor";
        //    door.transform.SetParent(transform);
        //    door.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //    door.transform.localPosition = new Vector3(-spriteBounds.size.x/2 + door.transform.localScale.x / 2, -spriteBounds.size.y/2 + floor.size.y + door.transform.localScale.y, transform.localScale.z);
        //    door.connectedRoom = leftRoom;
        //    doors.Add(door);
        //}
        //if (rightRoom)
        //{
        //    Door door = Instantiate(doorPrefab);
        //    door.name = "RightDoor";
        //    door.transform.SetParent(transform);
        //    door.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        //    door.transform.localPosition = new Vector3(spriteBounds.size.x/2 - door.transform.localScale.x / 2, -spriteBounds.size.y/2 + floor.size.y + door.transform.localScale.y, transform.position.z);
        //    door.connectedRoom = rightRoom;
        //    doors.Add(door);

        //}

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnMouseUp()
    {
        Debug.Log(name);

        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 localCoordinates = transform.InverseTransformPoint(clickPosition);

        OnClick?.Invoke(localCoordinates, this);
    }
    public float GetConnection(Room secondRoom)
    {
        return 1;
    //    switch (direction)
    //    {
    //        case "left":
    //            {
    //                return 1;
    //            }
    //        case "right":
    //            {
    //                return 1;
    //            }
    //        case "up":
    //            {
    //                return 1;
    //            }
    //        case "down":
    //            {
    //                return 1;
    //            }
    //        default:
    //            {
    //                return 0f;
    //            }
    //    }
    }

}
