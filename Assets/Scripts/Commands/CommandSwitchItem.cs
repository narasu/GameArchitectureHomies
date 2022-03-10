using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSwitchItem : ICommandKey
{
    Inventory inventory;
    InventorySlot slot;

    public CommandSwitchItem(Inventory _inventory, InventorySlot _slot)
    {
        inventory = _inventory;
        slot = _slot;
    }

    public void Execute()
    {
        inventory.pActiveSlot = slot;
    }
}
