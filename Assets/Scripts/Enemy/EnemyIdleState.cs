using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : AbstractState<Enemy>
{
    private FiniteStateMachine<Enemy> owner;
    public EnemyIdleState(FiniteStateMachine<Enemy> _owner)
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

        //What to do with this??? 
        //owner.pOwner.CalculateDistanceToTarget(owner.pOwner.target);
    }
}
