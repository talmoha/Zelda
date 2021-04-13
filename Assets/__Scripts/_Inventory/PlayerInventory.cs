using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    //player inventory list to store items
    public List<InventoryItem> myInventory = new List<InventoryItem>();
}
