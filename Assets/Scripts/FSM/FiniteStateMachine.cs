using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine<T>
{
    private AbstractState<T> currentState;
    private Dictionary<System.Type, AbstractState<T>> allStates = new Dictionary<System.Type, AbstractState<T>>();

    public T pOwner
    {
        get;
        protected set;
    }

    public FiniteStateMachine(T _owner)
    {
        pOwner = _owner;
    }

    public void AddState(AbstractState<T> _abstractState)
    {
        allStates.Add(_abstractState.GetType(), _abstractState);
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type _type)
    {
        currentState?.OnExit();
        if (allStates.ContainsKey(_type))
        {
            currentState = allStates[_type];
        }
        currentState?.OnEnter();
    }
}
