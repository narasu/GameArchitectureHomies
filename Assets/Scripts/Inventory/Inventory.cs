using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IControllable
{
    public InputManager pInputManager
    {
        get; private set;
    }

    public InventorySlot pActiveSlot
    {
        set
        {
            if (activeSlot != value)
            {
                activeSlot = value;
                fsm.SwitchState(value.state);
            }
        }
    }

    private InventorySlot activeSlot;

    private List<InventorySlot> slots = new List<InventorySlot>()
    {
        new InventorySlot { item = null, state = typeof(InventorySlot1State) },
        new InventorySlot { item = null, state = typeof(InventorySlot2State) },
        new InventorySlot { item = null, state = typeof(InventorySlot3State) }
    };

    private FiniteStateMachine<Inventory> fsm;


    void Start()
    {
        FindInputManager();

        fsm = new FiniteStateMachine<Inventory>(this);
        fsm.AddState(new InventorySlot1State(fsm, slots[0]));
        fsm.AddState(new InventorySlot2State(fsm, slots[1]));
        fsm.AddState(new InventorySlot3State(fsm, slots[2]));

        pInputManager.BindKey(KeyCode.Alpha1, new CommandSwitchItem(this, slots[0]));
        pInputManager.BindKey(KeyCode.Alpha2, new CommandSwitchItem(this, slots[1]));
        pInputManager.BindKey(KeyCode.Alpha3, new CommandSwitchItem(this, slots[2]));

        pActiveSlot = slots[0];
    }

    private void Update()
    {
        fsm.Update();
    }

    public void AddItemToSlot(IEquipable _item, int _slot)
    {
        if (slots.Count <= _slot)
        {
            Debug.Log($"Slot {_slot} does not exist!");
            return;
        }
        slots[_slot].item = _item;
    }

    public void AddItems(params IEquipable[] _items)
    {
        foreach (IEquipable i in _items)
        {
            InventorySlot slot = slots.Find(x => x.IsEmpty());
            Debug.Log(slot);
            if (slot == null)
            {
                Debug.Log("No empty slots found!");
                return;
            }
            slot.item = i;
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

