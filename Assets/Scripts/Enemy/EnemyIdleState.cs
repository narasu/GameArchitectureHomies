using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyIdleState : AbstractState<Enemy>
{
    private FiniteStateMachine<Enemy> owner;
    private Transform target;
    //private Transform[] destinations;
    private int currentPoint;
    public EnemyIdleState(FiniteStateMachine<Enemy> _owner, Transform _target)
    {
        owner = _owner;
        target = _target;
    }

    public override void OnEnter()
    {
        BackToPath();
        Debug.Log("enter Idle State");
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        CalculateDistance(target, owner.pOwner.destinations);
    }

    public void CalculateDistance(Transform _target, params Transform[] _destination)
    {
        Transform currentDestinationPoint = _destination[currentPoint];
        float distanceTo = Vector3.Distance(owner.pOwner.transform.position, _target.position);
        float distanceToPoint = Vector3.Distance(owner.pOwner.transform.position, currentDestinationPoint.position);
        if (distanceTo <= owner.pOwner.attackRadius)
        {
            owner.pOwner.timer += Time.deltaTime;
            if (owner.pOwner.timer > owner.pOwner.maxTime)
            {
                owner.pOwner.isInRange = true;
                owner.pOwner.enemyFSM.SwitchState(typeof(EnemyAttackState));
            }


        }
        else if (distanceTo > owner.pOwner.attackRadius)
        {
            owner.pOwner.isInRange = false;
            BackToPath();
        }

    }

    public void BackToPath()
    {
        if (owner.pOwner.isInRange == false && owner.pOwner.navMesh.remainingDistance < 8.5f)
        {
            int i = currentPoint;
            Vector3 moveTo = Vector3.MoveTowards(owner.pOwner.transform.position, owner.pOwner.destinations[i].position, 100f);
            owner.pOwner.navMesh.destination = moveTo;
            if (owner.pOwner.navMesh.remainingDistance <= 5f)
            {
                UpdateCurrentpoint();
            }
        }
           
    }

    public void UpdateCurrentpoint()
    {
        if (currentPoint == owner.pOwner.destinations.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
    }
}
