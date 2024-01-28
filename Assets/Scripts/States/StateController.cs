using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateController
{
    private Dictionary<States, State> allStates;
    public States currentState { get; private set; }
    // Start is called before the first frame update
    public void Init(States firstState, Dictionary<States,State> states)
    {
        currentState = firstState;
        allStates = states;
        allStates[currentState].Enter();
    }

    public void ChangeCurrentState(States newState)
    {
        if (!allStates.ContainsKey(newState))
        {
            return;
        }

        if (allStates[currentState] != null)
        {
            allStates[currentState].Exit();
        }
        currentState = newState;
        allStates[currentState].Enter();
    }

    public void Do()
    {
        allStates[currentState].Do();
    }
}

public class StateController<T>
{
    private Dictionary<States, State<T>> allStates = new Dictionary<States, State<T>>();
    public T data;
    public States currentState { get; private set; }
    // Start is called before the first frame update
    public void AddState<T1>(States state)  where T1 : State<T>,new ()
    {
        allStates.Add(state, new T1());
    }

    public void ChangeCurrentState(States newState)
    {
        if (!allStates.ContainsKey(newState))
        {
            return;
        }

        if (allStates[currentState] != null)
        {
            allStates[currentState].Exit(data);
        }
        currentState = newState;
        allStates[currentState].Enter(data);
    }

    public void Do()
    {
        allStates[currentState].Do(data);
    }
}

