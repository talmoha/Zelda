using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public GameObject qestionMark;//the question mark sign above player head

    public void Enable()//turning on question mark sign
    {
        qestionMark.SetActive(true);
    }

    public void Disable()//turning off question mark sign
    {
        qestionMark.SetActive(false);
    }
}
