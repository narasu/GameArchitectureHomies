using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AbstractState<Enemy>
{
    private FiniteStateMachine<Enemy> owner;
    private Transform target;

    public EnemyAttackState(FiniteStateMachine<Enemy> _owner, Transform _target)
    {
        owner = _owner;
        target = _target;
    }

    //Give reference to player target?
    public override void OnEnter()
    {
        Debug.Log("enter attACK STATE");
        owner.pOwner.movementSpeed += 10f;
    }

    public override void OnExit()
    {
        owner.pOwner.movementSpeed -= 10f;
    }

    public override void OnUpdate()
    {

        Attack(target);

    }

    public void Attack(Transform _target)
    {
        owner.pOwner.transform.LookAt(target);
        Vector3 moveTo = Vector3.MoveTowards(owner.pOwner.transform.position, _target.position, 100f);
        owner.pOwner.navMesh.destination = moveTo;

        float distanceTo = Vector3.Distance(owner.pOwner.transform.position, _target.position);
        if (distanceTo > owner.pOwner.attackRadius)
        {
            owner.pOwner.isInRange = false;
            
            owner.pOwner.enemyFSM.SwitchState(typeof(EnemyIdleState));
        }


    }
}
