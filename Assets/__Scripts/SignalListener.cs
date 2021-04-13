using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//we are using the events system


public class SignalListener : MonoBehaviour
{
    public Signal signal;//the signal it is going to listen to 
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        //when the signal is raised, invoke the event
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);//adding signal from system
    }
    private void OnDisable()
    {
        signal.DeRegisterListener(this);//removing signal from system
    }
}
