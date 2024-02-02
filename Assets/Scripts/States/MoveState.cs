using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private float speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float pathLength;
    private float totalTimeForPath;
    public MoveState(IUnit unit, StateController controller) : base(unit, controller)
    {
    }
    private void distanceCalculation(Vector3 startPos,Vector3 endPos)
    {
        //Обчислюємо координати двух точок, між якими буде здійснюватись рух

        //Визначаємо відстань між ними, та час що буде потрачений на нього
        pathLength = Vector2.Distance(startPos, endPos);
        totalTimeForPath = pathLength / speed;

    }
    public override void Enter()
    {
        //startPosition = _unit.transform.position;
        //speed = _unit.moveSpeed;
        //pathLength = Vector2.Distance(startPos, endPos);
        //totalTimeForPath = pathLength / speed;
    }
    public override void Do()
    {

       // _unit.transform.position = Vector2.Lerp(startPosition, endPosition, Time.deltaTime / totalTimeForPath);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
public class Data
{
    public IUnit unit;
    public LinkedList<Room> path = null;
    public LinkedListNode<Room> currentRoom = null;
    public List<IUnit> unitList = new List<IUnit>();
    public IUnit currentTarget = null;
    public Room currentDestination = null;
    public float finalPositionX;
}

public class MoveStateT : State<Data>
{
    private float speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float totalTimeForPath;
    private float lastWaypointSwitchTime;

    //private Data data;
    public MoveStateT() : base()
    {
    }
    private void distanceCalculation(Data data)
    {
        float pathLength;
        startPosition = data.unit.GetTransform().localPosition;

        if (data.currentDestination == data.currentRoom.Value)
        {
            endPosition = new Vector3(data.finalPositionX, startPosition.y, startPosition.z);
        }
        else
        {
            Door targetDoor = data.currentRoom.Value.GetDoorByDestination(data.currentDestination);
            endPosition = new Vector3(targetDoor.transform.localPosition.x, startPosition.y, startPosition.z);
            
        }
        pathLength = Vector2.Distance(startPosition, endPosition);
        totalTimeForPath = pathLength / speed;
        
        //Debug.Log("Time for path" + totalTimeForPath);

    }
    public override void Enter(Data data)
    {
        lastWaypointSwitchTime = Time.time;
        speed = data.unit.Speed;
        distanceCalculation(data);
          
        
        
    }
    public override void Do(Data data)
    {
        //Debug.Log("Start position" + startPosition);
        //Debug.Log("End position" + endPosition);
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        //Debug.Log("NextPoss" + Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath));
        if(startPosition!=endPosition)
        {
            data.unit.GetTransform().transform.localPosition = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        }
    }
    public override void Exit(Data data)
    {
        base.Exit(data);
    }
    public override void OnDoorEnter(Data data)
    {
        //Debug.LogError("current room"+data.currentRoom.Value.transform.position);
        //Debug.LogError("current destination"+data.currentDestination.transform.position);
        if (data.currentDestination == null)
        {
            Debug.LogError("current Dest is null");
        }
        if (data.path.Last.Value == null)
        {
            Debug.LogError("last value is null");
        }
        if (data.currentRoom.Value == null)
        {
            Debug.LogError("Curent room is null");
        }

        data.currentRoom = data.currentRoom.Next;
        if(data.currentDestination == data.path.Last.Value)//exception
        {
            data.currentDestination = data.currentRoom.Value;//exception
        }
        else
        {
            data.currentDestination = data.currentRoom.Next.Value;
        }
        lastWaypointSwitchTime = Time.time;
        distanceCalculation(data);
    }

}
