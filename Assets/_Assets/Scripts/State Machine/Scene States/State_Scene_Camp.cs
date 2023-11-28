using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Scene_Camp : State
{
    public State_Scene_Camp(StateMachine stateMachine) : base(stateMachine) { }

    public override string GetName()
    {
        return "State_Scene_Camp";
    }

    public override void TryStateTransition(IState state)
    {
        ExecuteStateTransition(state);
    }
}
