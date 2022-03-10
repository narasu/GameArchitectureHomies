using UnityEngine;
public interface IEquipable
{
    GameObject pObject
    {
        get;
    }

    void OnPickup();

    void OnEquip();
}