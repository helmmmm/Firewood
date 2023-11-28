using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Scene_Home : State
{
    public State_Scene_Home(StateMachine stateMachine) : base(stateMachine) { }

    public override string GetName()
    {
        return "State_Scene_Home";
    }

    public override void TryStateTransition(IState state)
    {
        ExecuteStateTransition(state);
    }
}
