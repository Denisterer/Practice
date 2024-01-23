using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{   
    protected IUnit _unit;
    protected StateController _controller;
    public States type;

    public State(IUnit unit, StateController controller)
    {
        _unit = unit;
        _controller = controller;
    }

    public virtual void Do() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
}
