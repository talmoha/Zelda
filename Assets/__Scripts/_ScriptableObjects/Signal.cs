using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject//momory of things to keep into account over time
{
    public List<SignalListener> listeners=new List<SignalListener>();

    public void Raise()
    {
        for(int i=listeners.Count -1;i>=0;i--)//looping over the listeners list, starting at the end of the list
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)//registering signal in system taking in a signal listener
    {
        listeners.Add(listener); //add that listener to the listeners list
    }

    public void DeRegisterListener(SignalListener listener)//deregistering signal in system taking in a signal listener
    {
        listeners.Remove(listener); //remove that listener from the listeners list
    }
}
