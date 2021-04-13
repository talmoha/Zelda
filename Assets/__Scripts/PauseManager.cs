using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;
    public GameObject inventoryPanel;
    public bool usingPausePanel;

    // Start is called before the first frame update
    void Start()
    {
        //sets variables to false
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if p is clicked, show pause screen
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
            if (isPaused)
        {//displays pause screen and pauses game 
            pausePanel.SetActive(true);
                Time.timeScale = 0f;
                usingPausePanel = true;
            }
            else
            {//resumes game and doesn't display pause screen anymore
            inventoryPanel.SetActive(false);
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
    }
   
    public void QuitToMain()
    {//changes to mainScene
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {//switches from pause panel to inventory panel
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }
}
