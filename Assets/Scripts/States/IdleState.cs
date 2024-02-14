using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class IdleStateT: State<Data>
{
    public IdleStateT() : base()
    {
    }

    public override async void Enter(Data data)
    {
        if(data.unit.canMoveAround)
        {
            await DelayToMoving(data);
        }
    }
    public override void Do(Data data)
    {
        Debug.Log("Idling");
    }
    public override void Exit(Data data) 
    {
        base.Exit(data);
    }
    async private Task DelayToMoving(Data data)
    {
        System.Random random = new System.Random();
        int time = random.Next(3000, 15000);
        await Task.Delay(time);
        data.unit.MakeRandomMove();
    }
}
