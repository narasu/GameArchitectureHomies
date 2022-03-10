using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    InputManager pInputManager { get; }
    void FindInputManager();
}
