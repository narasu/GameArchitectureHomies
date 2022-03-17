using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventorySlotState : AbstractState<Inventory>
{
    protected FiniteStateMachine<Inventory> owner;
    protected InventorySlot slot;

    public InventorySlotState(FiniteStateMachine<Inventory> _owner, InventorySlot _slot)
    {
        owner = _owner;
        slot = _slot;
    }

    public override void OnEnter()
    {
        Debug.Log(slot.item);
        slot.item.pObject?.SetActive(true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        slot.item.pObject.SetActive(false);
    }
}
