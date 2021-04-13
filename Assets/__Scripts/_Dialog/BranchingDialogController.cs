using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas; //the whole canvas so we can turn it on when it's active
    [SerializeField] private GameObject dialogPrefab; //the prefab for the dialog
    [SerializeField] private GameObject choicePrefab; //the prefab for the button
    [SerializeField] private TextAssetValue dialogValue; //the whole dialog
    [SerializeField] private Story myStory; //the story object of the dialog in order to traverse the story
    [SerializeField] private GameObject dialogHolder; //the object that holds the dialogs
    [SerializeField] private GameObject choiceHolder; //the object that holds the buttons
    [SerializeField] private ScrollRect dialogScroll; //need to do some crazy scroll thing to auto make it go to bottom


    [SerializeField] private GameObject troll; //need to do some crazy scroll thing to auto make it go to bottom


    public static BranchingDialogController S; //singleton

    private void Awake() //awake for singleton
    {
        if (S == null)
        {
            S = this; //set the singleton
        } else
        {
            Debug.LogError("Error: Singleton already created");
        }
    }


    public TextAssetValue DialogValue //property for setting the dialog value
    {
        set
        {
            dialogValue = value;
        }
    }

    public void EnableCanas() //called from the signal, starts the story
    {
        branchingCanvas.SetActive(true); //show the ui
        SetStory(); //set the story
        RefreshView(); //show the story
    }

    public void SetStory() //creates new story
    {
        if (dialogValue.value) //if a story already exists
        {
            DeleteOldDialogs(); //delete it
            myStory = new Story(dialogValue.value.text); //create a new one
        } else
        {
            Debug.Log("Something went wrong with the story setup");
        }
    }

    void DeleteOldDialogs() //deletes all the old dialogs
    {
        for(int i = 0; i < dialogHolder.transform.childCount; i++) //iterate throuth all the dialogs
        {
            Destroy(dialogHolder.transform.GetChild(i).gameObject); //destroy it
        }
    }


    public void RefreshView() //refreshes the dialog once you press a choice
    {

        while(myStory.canContinue) //while there is dialog to show
        {
            MakeNewDialog(myStory.Continue()); //create a new dialog

            if (myStory.currentTags.Count > 0 && myStory.currentTags[0] == "AllCorrect") //if that that dialog had all the answers correct
            {
                troll.SetActive(false);
            }

        }
        if (myStory.currentChoices.Count >0) //if there is choices associated with the dialog
        {
            MakeNewChoices(); //create all the choices
        }

        else //there is no choices associated with the dialog
        {
            branchingCanvas.SetActive(false);  //don't need to show the dialog
        }

        StartCoroutine(ScrollCo()); //used to scroll the dialog down
    }

    IEnumerator ScrollCo() //takes a frame to scroll down because unity is weird
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }


    void MakeNewDialog(string newDialog) //creates new dialog object
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void MakeNewResponse(string newDialog, int choiceValue) //create new button responce object
    {
        ResponseObject newResponseObject = Instantiate(choicePrefab, choiceHolder.transform).GetComponent<ResponseObject>();
        newResponseObject.Setup(newDialog, choiceValue);
        Button responseButton = newResponseObject.gameObject.GetComponent<Button>();
        if (responseButton) //if it exists
        {
            responseButton.onClick.AddListener(delegate { ChooseChoice(choiceValue); }); //delete to know what you chose
        }
    }


    void MakeNewChoices() //creates all the choices
    {
        for (int i =0; i < choiceHolder.transform.childCount; i++) //delete old choices
        {
            Destroy(choiceHolder.transform.GetChild(i).gameObject);

        }
        for (int i =0; i < myStory.currentChoices.Count; i++) //create new choices
        {
            MakeNewResponse(myStory.currentChoices[i].text, i);
        }
    }

    void ChooseChoice(int choice) //when you click a button
    {
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }
}
