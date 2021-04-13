using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver//lives outside scene and not attached to anything in scene, so it can be used in multiple scenes
{
    public float initialValue;

    [HideInInspector]
    public float RuntimeValue;

    public void OnAfterDeserialize()//after unloading game
    {
        RuntimeValue=initialValue;
    }

    public void OnBeforeSerialize(){}
}
