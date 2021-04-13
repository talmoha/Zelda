using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;
    private bool isPaused;
    public GameObject inventoryCanvas;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        //sets the description text to the description
        descriptionText.text = description;
        //if usable, show use button, else no use button appears
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        //creates slot in the inventory panel
        if (playerInventory)
        {
            //go through player inventory, checking items and creating slot
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                //doesn't show items with 0 items
                if (playerInventory.myInventory[i].numberHeld > 0)
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //creates inventory slots and clears description
        MakeInventorySlots();
        SetTextAndButton("", false);
        //keeps inventory canvas inactive and sets paused variable to false
        inventoryCanvas.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //shows inventory button when i is clicked 
        if (Input.GetButtonDown("inventory"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {//shows inventory panel and pauses game in background
                inventoryCanvas.SetActive(true);
                Time.timeScale = 0f;

            }
            else
            {//closes inventory panel and resumes game
                inventoryCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void ClosePanel()
    { //closes the panel
        inventoryCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem)
    {//sets up button and description for each item
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    void ClearInventorySlots()
    {//clears the slots when item is used up
        for (int i=0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            //clear inv slots
            ClearInventorySlots();
            //refill inv with new numbers
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
