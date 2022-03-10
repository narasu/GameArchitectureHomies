using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AbstractState<Enemy>
{
    private FiniteStateMachine<Enemy> owner;

    public EnemyAttackState(FiniteStateMachine<Enemy> _owner)
    {
        owner = _owner;
    }

    //Give reference to player target?
    public override void OnEnter()
    {
        owner.pOwner.movementSpeed += 10f;        
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        /* walk towards target(player) */
        //Shoot player

    }
}
