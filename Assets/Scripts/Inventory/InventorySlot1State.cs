using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot1State : AbstractState<Inventory>
{
    private FiniteStateMachine<Inventory> owner;

    public InventorySlot1State(FiniteStateMachine<Inventory> _owner)
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
