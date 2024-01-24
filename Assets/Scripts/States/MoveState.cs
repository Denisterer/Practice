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
        type = States.Move;
    }

    public override void Enter()
    {
        //startPosition = _unit.transform.position;
        //speed = _unit.moveSpeed;
        base.Enter();
    }
    public override void Do()
    {
        base.Do();
    }
    public override void Exit()
    {
        base.Exit();
    }

}
