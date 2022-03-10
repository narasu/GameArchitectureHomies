using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DamagePackage component is meant to be added to any object that can deal any kind of damage, such as a weapon or monster.
/// It is required by the IDamager interface. It allows setting damage values of different types,
/// as well as an IDamageable target that this damage should be dealt to. 
/// Multipliers can be set to each damage type. Total damage can be applied to any valid target at any time using the Execute method.
/// </summary>
public class DamagePackage
{
    class DamageValue
    {
        private float multiplier;

        public float Value { get; set; }
        
        public DamageValue(float _value, float _multiplier = 1.0f)
        {
            Value = _value;
            multiplier = _multiplier;
        }
        public void SetMultiplier(float _multiplier) => multiplier = _multiplier;
    }
    private IDamageable target;

    private Dictionary<DamageType, DamageValue> damageValues = new Dictionary<DamageType, DamageValue>();
    public DamagePackage(IDamageable _target)
    {
        target = _target;
    }

    public DamagePackage(IDamageable _target, params KeyValuePair<DamageType, float>[] _damage)
    {
        target = _target;
        Add(_damage);
    }

    public DamagePackage(params KeyValuePair<DamageType, float>[] _damage)
    {
        Add(_damage);
    }

    public void Add(params KeyValuePair<DamageType,float>[] _damage)
    {
        foreach(KeyValuePair<DamageType,float> kvp in _damage)
        {
            if (damageValues.ContainsKey(kvp.Key))
            {
                damageValues[kvp.Key].Value += kvp.Value;
                continue;
            }
            damageValues.Add(kvp.Key, new DamageValue(kvp.Value));
        }
    }

    public void Subtract(params KeyValuePair<DamageType, float>[] _damage)
    {
        foreach (KeyValuePair<DamageType, float> kvp in _damage)
        {
            if (!damageValues.ContainsKey(kvp.Key))
            {
                Debug.LogWarning("Package does not contain damage of type " + kvp.Key);
                continue;
            }
            damageValues[kvp.Key].Value -= kvp.Value;
        }
    }

    public void SetDamageOfType(DamageType _damageType, float _damage)
    {
        if (!damageValues.ContainsKey(_damageType))
        {
            damageValues.Add(_damageType, new DamageValue(_damage));
            return;
        }
        damageValues[_damageType].Value = _damage;
    }

    public void SetMultiplierOfType(DamageType _damageType, float _multiplier)
    {
        damageValues[_damageType].SetMultiplier(_multiplier);
    }

    public void ClearDamage() => damageValues.Clear();

    public void SetTarget(IDamageable _target) => target = _target;

    public void ClearTarget() => target = null;

    public void Execute()
    {
        if (target == null)
        {
            Debug.LogWarning("No target!");
            return;
        }

        foreach (KeyValuePair<DamageType, DamageValue> kvp in damageValues)
        {
            float newDamage = kvp.Value.Value - target.Resistances[kvp.Key];
            if (newDamage <= 0f)
            {
                Debug.Log("target is immune to damage of type " + kvp.Key + "!");

                //could be interesting to make the enemy heal if resistance exceeds damage
                newDamage = 0f;
            }
            target.TakeDamage(newDamage, kvp.Key);
        }
    }
}
