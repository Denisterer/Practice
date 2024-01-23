using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(IUnit unit, StateController controller) : base(unit, controller)
    {
        type = States.Attack;
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
