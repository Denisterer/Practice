using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(IUnit unit, StateController controller) : base(unit, controller)
    {
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
