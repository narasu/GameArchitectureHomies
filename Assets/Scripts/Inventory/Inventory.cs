using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IControllable
{
    private List<InventorySlot> items = new List<InventorySlot>()
    {
        new InventorySlot { item = null, state = typeof(InventorySlot1State) },
        new InventorySlot { item = null, state = typeof(InventorySlot1State) }
    };
    public InventorySlot pActiveSlot
    {
        set
        {
            activeSlot = value;
            fsm.SwitchState(activeSlot.state);
        }
    }
    private InventorySlot activeSlot;

    private FiniteStateMachine<Inventory> fsm;

    public InputManager pInputManager
    {
        get; private set;
    }

    void Awake()
    {
        fsm = new FiniteStateMachine<Inventory>(this);
        fsm.AddState(new InventorySlot1State(fsm));
        fsm.SwitchState(typeof(InventorySlot1State));

        pInputManager.BindKey(KeyCode.Alpha1, new CommandSwitchItem(this, items[0]));
    }

    public void AddItemToSlot(IEquipable _item, int _slot)
    {
        if (items.Count <= _slot)
        {
            Debug.Log($"Slot {_slot} does not exist!");
            return;
        }
        items[_slot].item = _item;
    }

    public void AddItems(params IEquipable[] _items)
    {
        foreach (IEquipable i in _items)
        {
            InventorySlot slot = items.Find(x => x.IsEmpty());
            if (slot == null)
            {
                Debug.Log("No empty slots found!");
                return;
            }
            //Debug.Log(slot.keyCode);
            //items.Add();
        }
    }

    public void FindInputManager()
    {
        pInputManager = GameObject.FindObjectOfType<InputManager>();
    }

    public void RemoveItem(IEquipable _item)
    {
        //items.Remove(_item);
    }
}

public class InventorySlot
{
    public IEquipable item;
    public System.Type state;

    public bool IsEmpty()
    {
        return item == null;
    }
}