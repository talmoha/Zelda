using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelPanelManager : MonoBehaviour
{
    private bool isNew;//is the level new
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
    }

    public void Play()
    {
        panel.SetActive(false);   
    }
}
