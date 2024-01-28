using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    public event Action<States> OnCallStateChange;
    protected IUnit _unit;
    protected StateController _controller;

    public State(IUnit unit, StateController controller)
    {
        _unit = unit;
        _controller = controller;
    }

    public virtual void Do() {
        //OnCallStateChange.Invoke(States.Move);
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void OnDoorEnter() { }
}

public abstract class State<T>
{
    public event Action<States> OnCallStateChange;

    public State()
    {

    }

    public virtual void Do(T data)
    {
        //OnCallStateChange.Invoke(States.Move);
    }
    public virtual void Enter(T data) { }
    public virtual void Exit(T data) { }
    //public virtual void OnDoorEnter() { }
}
