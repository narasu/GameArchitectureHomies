using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventorySlot
{
    public IEquipable item;
    public System.Type state;

    public bool IsEmpty()
    {
        return item == null;
    }
}