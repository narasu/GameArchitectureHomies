using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent
{
    public float Value
    {
        get; set;
    }

    public HealthComponent(float _value)
    {
        Value = _value;
    }
}
