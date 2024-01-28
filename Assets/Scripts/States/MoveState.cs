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
}

public class MoveStateT : State<Data>
{
    private float speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float pathLength;
    private float totalTimeForPath;
    public MoveStateT() : base()
    {
    }
    private void distanceCalculation(Vector3 startPos, Vector3 endPos)
    {
        //Обчислюємо координати двух точок, між якими буде здійснюватись рух

        //Визначаємо відстань між ними, та час що буде потрачений на нього
        pathLength = Vector2.Distance(startPos, endPos);
        totalTimeForPath = pathLength / speed;

    }
    public override void Enter(Data data)
    {
        //startPosition = _unit.transform.position;
        //speed = _unit.moveSpeed;
        //pathLength = Vector2.Distance(startPos, endPos);
        //totalTimeForPath = pathLength / speed;
    }
    public override void Do(Data data)
    {

         //data.unit.GetTransform().position = Vector2.Lerp(startPosition, endPosition, Time.deltaTime / totalTimeForPath);
    }
    public override void Exit(Data data)
    {
        base.Exit(data);
    }

}
