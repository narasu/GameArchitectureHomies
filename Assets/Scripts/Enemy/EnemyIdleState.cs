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

    //public void BackToPath()
    //{
    //    if (!owner.pOwner.isInRange && owner.pOwner.navMesh.remainingDistance < 8.5f)
    //    {
    //        int i = owner.pOwner.currentPoint;
    //        Vector3 moveTo = Vector3.MoveTowards(owner.pOwner.transform.position, owner.pOwner.destinations[i].position, 100f);
    //        owner.pOwner.navMesh.destination = moveTo;
    //        if (owner.pOwner.navMesh.remainingDistance < 1f)
    //        {
    //            owner.pOwner.UpdateCurrentpoint();
    //        }
    //    }
    //}
}
