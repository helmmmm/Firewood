using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Scene : StateMachine
{
    private static SM_Scene _instance;
    public static SM_Scene Instance => _instance ??= new SM_Scene();

    // States
    private State_Scene_Home _stateHome;
    private State_Scene_Camp _stateCamp;
    private State_Scene_Map _stateMap;

    // Create the states
    public State_Scene_Home State_Scene_Home => _stateHome ??= new State_Scene_Home(this);
    public State_Scene_Camp State_Scene_Camp => _stateCamp ??= new State_Scene_Camp(this);
    public State_Scene_Map State_Scene_Map => _stateMap ??= new State_Scene_Map(this);

    public bool IsHomeState => GetCurrentState() is State_Scene_Home;
    public bool IsCampState => GetCurrentState() is State_Scene_Camp;
    public bool IsMapState => GetCurrentState() is State_Scene_Map;

    public void Initialize()
    {
        // set the initial state
        SetInitialState(State_Scene_Home);
    }
}
