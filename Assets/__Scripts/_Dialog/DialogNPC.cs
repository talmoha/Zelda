using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//to access dialog 



public class DialogNPC : MonoBehaviour
{


    [SerializeField] private TextAssetValue dialogValue; //holds the value of the dialog

    [SerializeField] private TextAsset myDialog; //holds the whole dialog text

    [SerializeField] private Signal branchingDialogSignal; //signal to start the dialog

    public Signal contextOn;//making the question mark signal on
    public Signal contextOff;//making the question mark signal off
    public GameObject dialogBox; //dialog box
    public bool playerInRange;//whether or not dialog box is active





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.O) && playerInRange)//if O key on keyboard is pressed and player is in range, then dialog appears 
        {
            if (dialogBox.activeInHierarchy)//check if dialog box is active
            {
                dialogBox.SetActive(false);//if active then deactivate
            }
            else
            {
                dialogValue.value = myDialog;
                branchingDialogSignal.Raise();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //check if other (player) is in range to open dialog box
    {
        if (other.CompareTag("Player"))
        {
            BranchingDialogController.S.DialogValue = dialogValue; //set the dialog to this NPC's dialog
            contextOn.Raise();
            playerInRange = true;//makes player in range
        }
    }

    private void OnTriggerExit2D(Collider2D other)//check if player is out of range to exit dialog box
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;//makes player out of range
            dialogBox.SetActive(false);
        }
    }
}
