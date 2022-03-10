using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamager, IEquipable, IControllable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float damage;
    [SerializeField] private DamageType damageType;
    [SerializeField] private float range;

    public DamagePackage Damage
    {
        get; private set;
    }

    public GameObject pObject
    {
        get 
        { 
            return gameObject; 
        }
    }

    public InputManager pInputManager 
    { 
        get; private set; 
    }

    private void Awake()
    {
        Damage = new DamagePackage(new KeyValuePair<DamageType, float>(damageType, damage));
    }

    public virtual void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, range))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                Damage.SetTarget(target);
                Damage.Execute();
                Damage.ClearTarget();
            }
        }
    }

    public void FindInputManager()
    {
        pInputManager = FindObjectOfType<InputManager>();
    }

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    public void OnEquip()
    {
        throw new System.NotImplementedException();
    }
}
