using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdleState : AbstractState<Drone>
{
    private FiniteStateMachine<Drone> owner;

    public DroneIdleState(FiniteStateMachine<Drone> _owner)
    {
        owner = _owner;
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
