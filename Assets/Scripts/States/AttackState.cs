using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateT : State<Data>
{
    public AttackStateT() : base()
    {
    }

    public override void Enter(Data data)
    {
        base.Enter(data);
    }
    public override void Do(Data data)
    {
        if (data.currentTarget != null)
        {
            float distance = Vector3.Distance(data.unit.GetTransform().position,data.currentTarget.GetTransform().position);
            Debug.Log("Distance " + distance + "range "+ data.unit.GetAttackRange());
            if(distance > data.unit.GetAttackRange()) 
            {
                LinkedList<Room> rooms = new LinkedList<Room>();
                rooms.AddFirst(data.currentRoom.Value);
                data.unit.SetMove(rooms, data.currentTarget.GetTransform().localPosition.x);
            }
            data.unit.PerformAttack(data.currentTarget.GetTransform().position);
        }
        
    }
    public override void Exit(Data data)
    {
        base.Exit(data);
    }

}
