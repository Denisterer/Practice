using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateController
{
    private Dictionary<States, State> allStates;
    public State currentState;
    // Start is called before the first frame update
    public void Init(State firstState, Dictionary<States,State> states)
    {
        currentState = firstState;
        allStates = states;
    }

    public void ChangeCurrentState(States newState)
    {
        currentState.Exit();
        allStates.TryGetValue(newState, out currentState);
        currentState.Enter();
    }
}

