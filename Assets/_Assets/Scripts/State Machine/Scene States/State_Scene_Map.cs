using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Scene_Map : State
{
    public State_Scene_Map(StateMachine stateMachine) : base(stateMachine) { }

    public override string GetName()
    {
        return "State_Scene_Map";
    }

    public override void TryStateTransition(IState state)
    {
        ExecuteStateTransition(state);
    }
}
