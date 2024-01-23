using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(IUnit unit, StateController controller) : base(unit, controller)
    {
        type = States.Chase;

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
