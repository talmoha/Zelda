using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCharacterUI : MonoBehaviour
{
    public GameObject customizationPanel; // refrences the panel
    public GameObject wizard;
    public GameObject knight;
    public bool appear;//boolean expression to decide if panel should dissapear

    // Start is called before the first frame update
    void Awake()
    {
        customizationPanel.SetActive(true); // appears at the start of the scene
        if (appear)
        {
            customizationPanel.SetActive(false); // does not appears at the start of the scene
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("customization"))
        {
            customizationPanel.SetActive(true);
        }
    }

    public void WizardChoice() //if wizard button is clicked
    {
        wizard.SetActive(true);//make wizard image visible and make knight image dissapear
        knight.SetActive(false);
        customizationPanel.SetActive(false);//closes UI after clicking
    }
    public void KnightChoice() //if knight button is clicked
    {
        knight.SetActive(true);//make knight image appear and wizard image dissapear
        wizard.SetActive(false);
        customizationPanel.SetActive(false);//closes UI after clicking
    }
}
