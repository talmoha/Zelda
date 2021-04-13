using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public Signal healthSignal;

    public void Use (int amountToIncrease)
    {//increases player health
        playerHealth.RuntimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
