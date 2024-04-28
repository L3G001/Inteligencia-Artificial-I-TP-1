using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    State currentState;
    Dictionary<StateID, State> states = new Dictionary<StateID, State>();

    public void AddState(StateID id, State state)
    {
        states.Add(id, state);
        state.fsm = this;
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public void ChangeState(StateID id)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[id];
        currentState.OnEnter();
    }



}
public enum StateID
{
    Idle,
    Patrol,
    Attack,
    Docking,
    LeavingDock,
    Selling
}   