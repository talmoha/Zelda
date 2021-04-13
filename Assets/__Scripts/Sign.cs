using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//to access dialog 

public class Sign : MonoBehaviour
{
    public Signal contextOn;//making the question mark signal on
    public Signal contextOff;//making the question mark signal off
    public GameObject dialogBox; //dialog box
    public Text dialogText;//text in dialog box
    public string dialog;//string to replace dialog text

    public GameObject pressSpaceBox; //box to tell player to press space for dialog box
    public Text pressSpaceText;//text in dialog box
    public string pressSpace;//string of "press space"

    public bool playerInRange;//whether or not dialog box is active


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)//if player is in range of dialog box, tell them to press space to make dialog box appear
        {
            pressSpaceBox.SetActive(true);//make the box appear
            pressSpaceText.text=pressSpace;//change text to "press space" dialog
        }else{
            pressSpaceBox.SetActive(false);//if player not in range, dont make box appear
        }

        if (Input.GetKeyDown(KeyCode.O) && playerInRange)//if O key on keyboard is pressed and player is in range, then dialog appears 
        {
            if (dialogBox.activeInHierarchy)//check if dialog box is active
            {
                dialogBox.SetActive(false);//if active then deactivate
            }else{
                dialogBox.SetActive(true);//if not active then activate dialog box
                dialogText.text=dialog;//change the text in dialog
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //check if other (player) is in range to open dialog box
    {
        if (other.CompareTag("Player"))
        {
            contextOn.Raise();
            playerInRange=true;//makes player in range
        }
    }

    private void OnTriggerExit2D(Collider2D other)//check if player is out of range to exit dialog box
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange=false;//makes player out of range
            dialogBox.SetActive(false);
        }
    }
}
