using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inv : ScriptableObject
{
    public Signal powerupSignal;
    public Item currentItem;
    public List<Item> items = new List<Item>();

    private int c;

    public int coins
    {
        set
        {
            c = value;
            powerupSignal.Raise();

        }

        get
        {
            return c;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if(!items.Contains(itemToAdd))//if item is not already in list then add to inventory
        {
            items.Add(itemToAdd);
        }
    }

}