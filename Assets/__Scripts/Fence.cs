using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public GameObject enemy1; //first enemy in level
    public GameObject enemy2; //first enemy in level
    public GameObject fence; //fence object

    void Update()
    {
        if ((!enemy1.activeSelf)&&(!enemy2.activeSelf))//if enemy is not there anymore
        {
            fence.SetActive(false);//then set fence to dissapear to move to boss battle
        }
    }
}
