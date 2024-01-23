using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(IUnit unit, StateController controller) : base(unit, controller)
    {
        type = States.Move;
    }

    public override void Enter()
    {
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
