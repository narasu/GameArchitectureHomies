using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    //IActivatable? 
    private FiniteStateMachine<Drone> droneFSM;


    public Drone()
    {
        droneFSM = new FiniteStateMachine<Drone>(this);

        droneFSM.AddState(new DroneIdleState(droneFSM));
    }
}
